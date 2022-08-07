using Notes.Core.Interafaces;
using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

/// <summary>
///     Заметка
/// </summary>
public sealed class Note : IHasHistory<NoteHistory>
{
    /// <summary>
    ///     Id заметки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Заголовок заметки
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    ///     Тело заметки
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    ///     Ярлыки заметок
    /// </summary>
    public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();

    public IReadOnlyCollection<NoteHistory> Histories { get; } = new List<NoteHistory>();
    public NoteHistory Access(IHasHistoryVisitor visitor)
    {
        return visitor.Visit(this);
    }
}