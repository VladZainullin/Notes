using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

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
    private readonly DbContext _context;
    private readonly IConfigurationProvider _provider;

    public GetLabelHandler(
        DbContext context,
        IConfigurationProvider provider)
    {
        _context = context;
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

        var dto = await GetDtoAsync(
            request.LabelId,
            cancellationToken);

        return dto;
    }

    private async Task<bool> IsExistsLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Label>()
            .AnyAsync(label => label.Id == labelId, cancellationToken);

        return exists;
    }

    private async Task<GetLabelDto> GetDtoAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Label>()
            .Where(label => label.Id == labelId)
            .ProjectTo<GetLabelDto>(_provider)
            .SingleAsync(cancellationToken);
    }
}