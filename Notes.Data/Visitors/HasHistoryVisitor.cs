using Notes.Core.Entities;
using Notes.Core.Interafaces;

namespace Notes.Data.Visitors;

public sealed class HasHistoryVisitor : 
    IHasHistoryVisitor
{
    public LabelHistory Visit(Label label)
    {
        var history = new LabelHistory
        {
            Title = label.Title,
            Label = label,
            DateOfModification = DateTime.Now
        };

        return history;
    }
}