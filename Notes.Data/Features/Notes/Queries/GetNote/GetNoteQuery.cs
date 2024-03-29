using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Queries.GetNote;

public sealed record GetNoteQuery(int NoteId) : IRequest<GetNoteDto>;

internal sealed class GetNoteHandler :
    IRequestHandler<GetNoteQuery, GetNoteDto>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetNoteHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
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

        var access = note.UserId == _currentUserService.Id;

        if (!access)
            throw new ForbiddenException("Заметка принадлежит другому пользователю");

        return _mapper.Map<GetNoteDto>(note);
    }

    private async Task<bool> IsExistsNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Notes
            .AnyAsync(n => n.Id == noteId, cancellationToken);

        return exists;
    }

    private async Task<Note> GetNoteAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .SingleAsync(n => n.Id == noteId, cancellationToken);

        return note;
    }
}