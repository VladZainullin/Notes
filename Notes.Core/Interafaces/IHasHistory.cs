namespace Notes.Core.Interafaces;

public interface IHasHistory<out THistory>
    where THistory : IHistory
{
    IReadOnlyCollection<THistory> Histories { get; }

    THistory Access(IHasHistoryVisitor visitor);
}