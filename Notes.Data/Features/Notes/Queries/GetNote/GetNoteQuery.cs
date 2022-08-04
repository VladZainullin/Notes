using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Notes.Queries.GetNote;

public sealed class GetNoteQuery : IRequest<GetNoteDto>
{
    public GetNoteQuery(int noteId)
    {
        NoteId = noteId;
    }

    public int NoteId { get; }
}

internal sealed class GetNoteHandler :
    IRequestHandler<GetNoteQuery, GetNoteDto>
{
    private readonly DbContext _context;
    private readonly IConfigurationProvider _provider;

    public GetNoteHandler(
        DbContext context,
        IConfigurationProvider provider)
    {
        _context = context;
        _provider = provider;
    }

    public async Task<GetNoteDto> Handle(
        GetNoteQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var note = await _context
                .Set<Note>()
                .AsNoTracking()
                .Where(n => n.Id == request.NoteId)
                .ProjectTo<GetNoteDto>(_provider)
                .SingleAsync(cancellationToken);

            return note;
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException("Заметка не найдена");
        }
    }
}