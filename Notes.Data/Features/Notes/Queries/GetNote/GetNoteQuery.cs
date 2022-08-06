using AutoMapper;
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
    private readonly IMapper _mapper;

    public GetNoteHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetNoteDto> Handle(
        GetNoteQuery request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(request.NoteId, cancellationToken);
        if (!exists)
            throw new NotFoundException("Заметка не найдена");

        var note = await GetNoteAsync(request.NoteId, cancellationToken);

        return _mapper.Map<GetNoteDto>(note);
    }

    private async Task<bool> IsExistsNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Note>()
            .AnyAsync(n => n.Id == noteId, cancellationToken);

        return exists;
    }

    private async Task<Note> GetNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<Note>()
            .SingleAsync(n => n.Id == noteId, cancellationToken);

        return note;
    }
}