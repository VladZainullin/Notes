using Notes.Core.Entities;

namespace Notes.Core.Interfaces;

public interface IHasHistoryVisitor
{
    LabelHistory Visit(Label entity);
    
    NoteHistory Visit(Note entity);
}