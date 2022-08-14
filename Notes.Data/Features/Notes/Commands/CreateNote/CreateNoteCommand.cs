using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Commands.CreateNote;

public sealed record CreateNoteCommand(CreateNoteDto Dto) : IRequest<int>;

internal sealed class CreateNoteHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly DbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateNoteHandler(
        DbContext context,
        CurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<int> Handle(
        CreateNoteCommand request,
        CancellationToken cancellationToken)
    {
        var note = _mapper.Map<Note>(request.Dto);
        note.UserId = _currentUserService.Id;

        await _context.AddAsync(note, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}