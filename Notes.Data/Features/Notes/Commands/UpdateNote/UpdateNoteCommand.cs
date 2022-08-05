using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Notes.Commands.UpdateNote;

public sealed class UpdateNoteCommand : IRequest
{
    public UpdateNoteCommand(int id, UpdateNoteDto? updateNoteDto)
    {
        Id = id;
        UpdateNoteDto = updateNoteDto;
    }

    internal int Id { get; }
    internal UpdateNoteDto? UpdateNoteDto { get; set; }
}

internal sealed class UpdateNoteHandler : AsyncRequestHandler<UpdateNoteCommand>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public UpdateNoteHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected override async Task Handle(
        UpdateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsNoteAsync(
            request.Id,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую заметку");

        var note = await GetNoteAsync(
            request.Id,
            cancellationToken);

        _mapper.Map(request.UpdateNoteDto, note);
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