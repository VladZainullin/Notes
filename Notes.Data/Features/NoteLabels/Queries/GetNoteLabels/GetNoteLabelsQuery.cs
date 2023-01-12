using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.NoteLabels.Queries.GetNoteLabels;

public sealed record GetNoteLabelsQuery(int NoteId) :
    IRequest<IEnumerable<GetNoteLabelsDto>>;

internal sealed class GetNoteLabelsHandler :
    IRequestHandler<GetNoteLabelsQuery, IEnumerable<GetNoteLabelsDto>>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IConfigurationProvider _provider;

    public GetNoteLabelsHandler(
        AppDbContext context,
        CurrentUserService currentUserService,
        IConfigurationProvider provider)
    {
        _context = context;
        _currentUserService = currentUserService;
        _provider = provider;
    }

    public async Task<IEnumerable<GetNoteLabelsDto>> Handle(
        GetNoteLabelsQuery request,
        CancellationToken cancellationToken)
    {
        var dtos = await _context.NoteLabels
            .Where(n =>
                n.NoteId == request.NoteId
                &&
                n.Note!.UserId == _currentUserService.Id
                &&
                n.Label!.UserId == _currentUserService.Id)
            .ProjectTo<GetNoteLabelsDto>(_provider)
            .ToListAsync(cancellationToken);

        return dtos;
    }
}