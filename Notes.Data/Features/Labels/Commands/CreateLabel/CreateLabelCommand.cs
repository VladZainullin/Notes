using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Labels.Commands.CreateLabel;

public sealed record CreateLabelCommand(CreateLabelDto Dto) : IRequest<int>;

internal sealed class CreateLabelHandler : IRequestHandler<CreateLabelCommand, int>
{
    private readonly DbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateLabelHandler(
        DbContext context,
        CurrentUserService currentUserService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<int> Handle(
        CreateLabelCommand request,
        CancellationToken cancellationToken)
    {
        var label = _mapper.Map<Label>(request.Dto);
        label.UserId = _currentUserService.Id;

        await _context.AddAsync(label, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return label.Id;
    }
}