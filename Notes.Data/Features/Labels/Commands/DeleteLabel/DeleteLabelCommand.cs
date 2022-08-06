using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Labels.Commands.DeleteLabel;

public sealed record DeleteLabelCommand(int LabelId) : IRequest;

internal sealed class DeleteLabelHandler : AsyncRequestHandler<DeleteLabelCommand>
{
    private readonly DbContext _context;

    public DeleteLabelHandler(DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeleteLabelCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsLabelAsync(
            request.LabelId,
            cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления несуществующего ярлыка");

        var label = await GetLabelAsync(
            request.LabelId,
            cancellationToken);
        _context.Remove(label);

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