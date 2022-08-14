using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Commands.UnpinNote;

public sealed record UnpinNoteCommand(int NoteId) : IRequest;

internal sealed class UnpinNoteHandler : AsyncRequestHandler<UnpinNoteCommand>
{
    private readonly DbContext _context;
    private readonly CurrentUserService _currentUserService;

    public UnpinNoteHandler(
        DbContext context,
        CurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    protected override async Task Handle(
        UnpinNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(request.NoteId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка закрепить несуществующую заметку");

        var note = await GetNoteAsync(request.NoteId, cancellationToken);
        var access = note.Id == _currentUserService.Id;
        if (!access)
            throw new BadRequestException("Заметка принадлежит другому пользователю");

        note.IsPinned = false;

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