using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Labels.Queries.GetLabel;

public sealed record GetLabelQuery :
    IRequest<GetLabelDto>
{
    public GetLabelQuery(int labelId)
    {
        LabelId = labelId;
    }

    public int LabelId { get; }
}

internal sealed class GetLabelHandler :
    IRequestHandler<GetLabelQuery, GetLabelDto>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IConfigurationProvider _provider;

    public GetLabelHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IConfigurationProvider provider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _provider = provider;
    }

    public async Task<GetLabelDto> Handle(
        GetLabelQuery request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsLabelAsync(
            request.LabelId,
            cancellationToken);
        if (!exists)
            throw new NotFoundException("Ярлык не найден");

        var access = await _context
            .Labels
            .AnyAsync(label => label.Id == request.LabelId
                               &&
                               label.UserId == _currentUserService.Id, cancellationToken);
        if (!access)
            throw new ForbiddenException("Ярлык принадлежит другому пользователю");

        var dto = await GetDtoAsync(
            request.LabelId,
            cancellationToken);

        return dto;
    }

    private async Task<bool> IsExistsLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Labels
            .AnyAsync(label => label.Id == labelId, cancellationToken);

        return exists;
    }

    private async Task<GetLabelDto> GetDtoAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context.Labels
            .Where(label => label.Id == labelId)
            .ProjectTo<GetLabelDto>(_provider)
            .SingleAsync(cancellationToken);
    }
}