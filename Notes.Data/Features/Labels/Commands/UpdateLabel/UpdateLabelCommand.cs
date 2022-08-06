using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Labels.Commands.UpdateLabel;

public sealed record UpdateLabelCommand(
    int LabelId,
    UpdateLabelDto Dto) : IRequest;

internal sealed class UpdateLabelHandler : 
    AsyncRequestHandler<UpdateLabelCommand>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;

    public UpdateLabelHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    protected override async Task Handle(
        UpdateLabelCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsLabelAsync(
            request.LabelId,
            cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка обновления несуществующего ярлыка");
        
        var label = await GetLabelAsync(
            request.LabelId,
            cancellationToken);
        _mapper.Map(request.Dto, label);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Label>()
            .AsNoTracking()
            .AnyAsync(label => label.Id == labelId, cancellationToken);
    }

    private async Task<Label> GetLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Label>()
            .SingleAsync(label => label.Id == labelId, cancellationToken);
    }
}