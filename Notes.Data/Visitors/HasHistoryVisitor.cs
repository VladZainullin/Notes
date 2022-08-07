using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interfaces;

namespace Notes.Data.Visitors;

public sealed class HasHistoryVisitor : 
    IHasHistoryVisitor
{
    private readonly EntityState _state;

    public HasHistoryVisitor(EntityState state)
    {
        _state = state;
    }
    public LabelHistory Visit(Label entity)
    {
        var history = new LabelHistory
        {
            Title = entity.Title,
            Label = entity,
            DateOfModification = DateTime.Now,
            State = _state
        };

        return history;
    }

    public NoteHistory Visit(Note entity)
    {
        var history = new NoteHistory
        {
            Note = entity,
            Header = entity.Header,
            Body = entity.Body,
            DateOfModification = DateTime.Now,
            State = _state
        };

        return history;
    }
}