using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Commands.CreateLabel;

public sealed record CreateLabelCommand(CreateLabelDto Dto) : IRequest<int>;

internal sealed class CreateLabelHandler : IRequestHandler<CreateLabelCommand, int>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public CreateLabelHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(
        CreateLabelCommand request,
        CancellationToken cancellationToken)
    {
        var label = _mapper.Map<Label>(request.Dto);

        await _context.AddAsync(label, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return label.Id;
    }
}