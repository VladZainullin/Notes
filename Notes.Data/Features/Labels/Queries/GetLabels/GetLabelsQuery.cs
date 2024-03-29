using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Data.Contexts;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Labels.Queries.GetLabels;

public sealed record GetLabelsQuery :
    IRequest<IEnumerable<GetLabelsDto>>
{
}

internal sealed class GetLabelHandler :
    IRequestHandler<GetLabelsQuery, IEnumerable<GetLabelsDto>>
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

    public async Task<IEnumerable<GetLabelsDto>> Handle(
        GetLabelsQuery request,
        CancellationToken cancellationToken)
    {
        var dtos = await _context.Labels
            .Where(label => label.UserId == _currentUserService.Id)
            .ProjectTo<GetLabelsDto>(_provider)
            .ToListAsync(cancellationToken);

        return dtos;
    }
}