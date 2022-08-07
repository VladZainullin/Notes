using Notes.Core.Interafaces;

namespace Notes.Core.Entities;

/// <summary>
///     Ярлык
/// </summary>
public sealed class Label : IHasHistory<LabelHistory>
{
    /// <summary>
    ///     Id ярлыка
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Наименование ярлыка
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     Ярлыки заметок
    /// </summary>
    public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();
    
    public IReadOnlyCollection<LabelHistory> Histories { get; }
    public LabelHistory Access(IHasHistoryVisitor visitor)
    {
        return visitor.Visit(this);
    }
}