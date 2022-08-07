using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interafaces;

namespace Notes.Data.Visitors;

public sealed class HasHistoryVisitor : 
    IHasHistoryVisitor
{
    private readonly EntityState _state;

    public HasHistoryVisitor(EntityState state)
    {
        _state = state;
    }
    public LabelHistory Visit(Label label)
    {
        var history = new LabelHistory
        {
            Title = label.Title,
            Label = label,
            DateOfModification = DateTime.Now,
            State = _state
        };

        return history;
    }
}