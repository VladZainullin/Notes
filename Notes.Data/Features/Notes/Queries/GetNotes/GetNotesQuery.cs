using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Notes.Queries.GetNotes;

public sealed record GetNotesQuery : IRequest<IEnumerable<GetNotesDto>>;

internal sealed class GetNotesHandler :
    IRequestHandler<GetNotesQuery, IEnumerable<GetNotesDto>>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IConfigurationProvider _provider;

    public GetNotesHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IConfigurationProvider provider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _provider = provider;
    }

    public async Task<IEnumerable<GetNotesDto>> Handle(
        GetNotesQuery request,
        CancellationToken cancellationToken)
    {
        var notes = await _context.Notes
            .AsNoTracking()
            .Where(n => n.UserId == _currentUserService.Id)
            .ProjectTo<GetNotesDto>(_provider)
            .ToListAsync(cancellationToken);

        return notes;
    }
}