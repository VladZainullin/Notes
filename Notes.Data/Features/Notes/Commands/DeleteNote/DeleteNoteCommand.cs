using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Notes.Commands.DeleteNote;

public sealed class DeleteNoteCommand : 
    IRequest
{
    public DeleteNoteCommand(int noteId)
    {
        NoteId = noteId;
    }

    public int NoteId { get; }
}

internal sealed class DeleteNoteHandler : 
    AsyncRequestHandler<DeleteNoteCommand>
{
    private readonly DbContext _context;

    public DeleteNoteHandler(DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeleteNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(request.NoteId, cancellationToken);
        if (exists)
            throw new BadRequestException("Попытка удаления не существующей заметки");

        var note = await GetNoteAsync(request.NoteId, cancellationToken);

        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
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