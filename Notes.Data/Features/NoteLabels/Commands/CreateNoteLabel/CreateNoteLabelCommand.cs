using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.NoteLabels.Commands.CreateNoteLabel;

public sealed record CreateNoteLabelCommand(
    int NoteId,
    int LabelId) : IRequest;

internal sealed class CreateNoteLabelHandler :
    AsyncRequestHandler<CreateNoteLabelCommand>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public CreateNoteLabelHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
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

        var labelExists = await IsExistsLabelAsync(
            request.LabelId,
            cancellationToken);
        if (!labelExists)
            throw new BadRequestException("Ярлык не найден!");

        var noteLabelExists = await IsExistsNoteLabelsAsync(
            request,
            cancellationToken);
        if (noteLabelExists)
            throw new BadRequestException("Заметка уже помечена указанным ярлыком!");

        var noteLabel = _mapper.Map<NoteLabel>(request);

        await _context.AddAsync(noteLabel, cancellationToken);
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

    private async Task<bool> IsExistsLabelAsync(
        int noteId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Label>()
            .AnyAsync(n => n.Id == noteId, cancellationToken);

        return exists;
    }

    private async Task<bool> IsExistsNoteLabelsAsync(
        CreateNoteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<NoteLabel>()
            .AnyAsync(nl =>
                    nl.LabelId == request.LabelId
                    &&
                    nl.NoteId == request.NoteId,
                cancellationToken);

        return exists;
    }
}