using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Data.Features.NoteLabels.Queries.GetNoteLabels;

public sealed record GetNoteLabelsQuery(int NoteId) :
    IRequest<IEnumerable<GetNoteLabelsDto>>;

internal sealed class GetNoteLabelsHandler :
    IRequestHandler<GetNoteLabelsQuery, IEnumerable<GetNoteLabelsDto>>
{
    private readonly DbContext _context;
    private readonly IConfigurationProvider _provider;

    public GetNoteLabelsHandler(
        DbContext context,
        IConfigurationProvider provider)
    {
        _context = context;
        _provider = provider;
    }

    public async Task<IEnumerable<GetNoteLabelsDto>> Handle(
        GetNoteLabelsQuery request,
        CancellationToken cancellationToken)
    {
        var dtos = await _context
            .Set<NoteLabel>()
            .Where(n => n.NoteId == request.NoteId)
            .ProjectTo<GetNoteLabelsDto>(_provider)
            .ToListAsync(cancellationToken);

        return dtos;
    }
}