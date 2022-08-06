using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Queries.GetLabels;

public sealed record GetLabelsQuery : 
    IRequest<IEnumerable<GetLabelsDto>>
{
    
}

internal sealed class GetLabelHandler : 
    IRequestHandler<GetLabelsQuery, IEnumerable<GetLabelsDto>>
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
    
    public async Task<IEnumerable<GetLabelsDto>> Handle(
        GetLabelsQuery request,
        CancellationToken cancellationToken)
    {
        var dtos = await GetDtoAsync(cancellationToken);

        return dtos;
    }

    private async Task<IEnumerable<GetLabelsDto>> GetDtoAsync(
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Label>()
            .ProjectTo<GetLabelsDto>(_provider)
            .ToListAsync(cancellationToken);
    }
}