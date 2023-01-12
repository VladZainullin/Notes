using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Labels.Commands.DeleteLabel;

public sealed record DeleteLabelCommand(int LabelId) : IRequest;

internal sealed class DeleteLabelHandler : AsyncRequestHandler<DeleteLabelCommand>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;

    public DeleteLabelHandler(
        AppDbContext context,
        CurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
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

        var access = label.UserId == _currentUserService.Id;
        if (!access)
            throw new ForbiddenException("Ярлык принадлежит другому пользователю");

        _context.Labels.Remove(label);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context.Labels
            .AsNoTracking()
            .AnyAsync(label => label.Id == labelId, cancellationToken);
    }

    private async Task<Label> GetLabelAsync(
        int labelId,
        CancellationToken cancellationToken)
    {
        return await _context.Labels
            .AsTracking()
            .SingleAsync(label => label.Id == labelId, cancellationToken);
    }
}