using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.NoteLabels.Commands.DeleteNoteLabel;

public sealed record DeleteNoteLabelCommand(
    int NoteId,
    int LabelId) : IRequest;

internal sealed class DeleteNoteLabelHandler :
    AsyncRequestHandler<DeleteNoteLabelCommand>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public DeleteNoteLabelHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected override async Task Handle(
        DeleteNoteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteLabelsAsync(
            request,
            cancellationToken);
        if (!exists)
            throw new BadRequestException("Ярлык заметки не найден!");

        var noteLabel = _mapper.Map<NoteLabel>(request);

        _context.Remove(noteLabel);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsNoteLabelsAsync(
        DeleteNoteLabelCommand request,
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