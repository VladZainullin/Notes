using Notes.Core.Entities;

namespace Notes.Core.Interafaces;

public interface IHasHistoryVisitor
{
    LabelHistory Visit(Label entity);
}