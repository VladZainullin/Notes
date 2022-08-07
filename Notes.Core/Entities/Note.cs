using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

/// <summary>
///     Заметка
/// </summary>
public sealed class Note : IHasId
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
}