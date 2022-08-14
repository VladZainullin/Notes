using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Commands.DeleteNote;

public sealed record DeleteNoteCommand(int NoteId) : IRequest;

internal sealed class DeleteNoteHandler :
    AsyncRequestHandler<DeleteNoteCommand>
{
    private readonly DbContext _context;
    private readonly CurrentUserService _currentUserService;

    public DeleteNoteHandler(
        DbContext context,
        CurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    protected override async Task Handle(
        DeleteNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(request.NoteId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей заметки!");

        var note = await GetNoteAsync(request.NoteId, cancellationToken);
        var access = note.UserId == _currentUserService.Id;
        if (!access)
            throw new ForbiddenException("Заметка принадлежит другому пользователю!");

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