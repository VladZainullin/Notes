using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Notes.Commands.PinNote;

public sealed record PinNoteCommand(int NoteId) : IRequest;

internal sealed class PinNoteHandler : AsyncRequestHandler<PinNoteCommand>
{
    private readonly DbContext _context;

    public PinNoteHandler(DbContext context)
    {
        _context = context;
    }
    
    protected override async Task Handle(
        PinNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(request.NoteId, cancellationToken);
        if (!exists)
        {
            throw new BadRequestException("Попытка закрепить несуществующую заметку");
        }
        
        var note = await GetNoteAsync(request.NoteId, cancellationToken);
        note.IsPinned = true;

        await _context.SaveChangesAsync(cancellationToken);
    }
    
    private async Task<bool> IsExistsNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Note>()
            .AsNoTracking()
            .AnyAsync(n => n.Id == noteId, cancellationToken);

        return exists;
    }

    private async Task<Note> GetNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<Note>()
            .AsTracking()
            .SingleAsync(n => n.Id == noteId, cancellationToken);

        return note;
    }
}