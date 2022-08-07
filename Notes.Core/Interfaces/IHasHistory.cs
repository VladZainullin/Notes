namespace Notes.Core.Interfaces;

public interface IHasHistory<out THistory>
    where THistory : IHistory
{
    IReadOnlyCollection<THistory> Histories { get; }

    THistory Access(IHasHistoryVisitor visitor);
}