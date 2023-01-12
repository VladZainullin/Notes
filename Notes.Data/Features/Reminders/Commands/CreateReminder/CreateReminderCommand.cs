using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Reminders.Commands.CreateReminder;

public sealed record CreateReminderCommand(
    int NoteId,
    TimeOnly Time) : IRequest;

internal sealed class CreateReminderHandler : AsyncRequestHandler<CreateReminderCommand>
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUserService;

    public CreateReminderHandler(
        AppDbContext context,
        CurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    
    protected override async Task Handle(
        CreateReminderCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Reminders
            .AnyAsync(r =>
                r.NoteId == request.NoteId
                &&
                r.TimeOfInclusion == request.Time, cancellationToken);
        if (exists)
        {
            throw new BadRequestException("Напоминание на это время уже существует");
        }
        
        var reminder = new Reminder
        {
            NoteId = request.NoteId,
            TimeOfInclusion = request.Time
        };

        await _context.Reminders.AddAsync(reminder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}