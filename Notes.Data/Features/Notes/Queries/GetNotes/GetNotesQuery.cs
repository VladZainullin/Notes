using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Queries.GetNotes;

public sealed class GetNotesQuery : IRequest<IEnumerable<GetNotesDto>>
{
    
}

internal sealed class GetNotesHandler :
    IRequestHandler<GetNotesQuery, IEnumerable<GetNotesDto>>
{
    private readonly DbContext _context;
    private readonly IConfigurationProvider _provider;

    public GetNotesHandler(
        DbContext context,
        IConfigurationProvider provider)
    {
        _context = context;
        _provider = provider;
    }
    
    public async Task<IEnumerable<GetNotesDto>> Handle(
        GetNotesQuery request,
        CancellationToken cancellationToken)
    {
        var notes = await _context
            .Set<Note>()
            .AsNoTracking()
            .ProjectTo<GetNotesDto>(_provider)
            .ToListAsync(cancellationToken);

        return notes;
    }
}