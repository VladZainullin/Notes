using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Commands.CreateNote;

public sealed record CreateNoteCommand(CreateNoteDto Dto) : IRequest<int>;

internal sealed class CreateNoteHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public CreateNoteHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(
        CreateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = _mapper.Map<Note>(request.Dto);

        await _context.AddAsync(note, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}