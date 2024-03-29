using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.NoteLabels.Commands.CreateNoteLabel;

public sealed record CreateNoteLabelCommand(
    int NoteId,
    int LabelId) : IRequest;

internal sealed class CreateNoteLabelHandler :
    AsyncRequestHandler<CreateNoteLabelCommand>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateNoteLabelHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    protected override async Task Handle(
        CreateNoteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var noteExists = await IsExistsNoteAsync(
            request.NoteId,
            cancellationToken);
        if (!noteExists)
            throw new BadRequestException("Заметка не найдена!");

        var noteAccess = await GetNoteAccessAsync(
            request.NoteId,
            cancellationToken);
        if (!noteAccess)
            throw new ForbiddenException("Заметка принадлежит другому пользователю");

        var labelExists = await IsExistsLabelAsync(
            request.LabelId,
            cancellationToken);
        if (!labelExists)
            throw new BadRequestException("Ярлык не найден!");

        var labelAccess = await GetNoteAccessAsync(
            request.LabelId,
            cancellationToken);
        if (!labelAccess)
            throw new ForbiddenException("Ярлык принадлежит другому пользователю");

        var noteLabelExists = await IsExistsNoteLabelsAsync(
            request,
            cancellationToken);
        if (noteLabelExists)
            throw new BadRequestException("Заметка уже помечена указанным ярлыком!");

        var noteLabel = _mapper.Map<NoteLabel>(request);

        await _context.NoteLabels.AddAsync(noteLabel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> GetNoteAccessAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        return await _context.Notes
            .AnyAsync(n =>
                n.Id == noteId
                &&
                n.UserId == _currentUserService.Id, cancellationToken);
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

    private async Task<bool> IsExistsLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Labels
            .AnyAsync(label => label.Id == labelId, cancellationToken);

        return exists;
    }

    private async Task<bool> IsExistsNoteLabelsAsync(
        CreateNoteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.NoteLabels
            .AnyAsync(nl =>
                    nl.LabelId == request.LabelId
                    &&
                    nl.NoteId == request.NoteId,
                cancellationToken);

        return exists;
    }
}