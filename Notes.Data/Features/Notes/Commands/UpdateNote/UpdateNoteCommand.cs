using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Commands.UpdateNote;

public sealed record UpdateNoteCommand(int NoteId, UpdateNoteDto Dto) : IRequest;

internal sealed class UpdateNoteHandler : AsyncRequestHandler<UpdateNoteCommand>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UpdateNoteHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    protected override async Task Handle(
        UpdateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(
            request.NoteId,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую заметку");

        var note = await GetNoteAsync(
            request.NoteId,
            cancellationToken);

        var access = note.UserId == _currentUserService.Id;
        if (!access)
            throw new ForbiddenException("Заметка принадлежит другому пользователю");

        _mapper.Map(request.Dto, note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Notes
            .AsNoTracking()
            .AnyAsync(n => n.Id == noteId, cancellationToken);

        return exists;
    }

    private async Task<Note> GetNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .AsTracking()
            .SingleAsync(n => n.Id == noteId, cancellationToken);

        return note;
    }
}