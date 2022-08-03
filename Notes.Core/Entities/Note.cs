namespace Notes.Core.Entities;

/// <summary>
/// Заметка
/// </summary>
public sealed class Note
{
    /// <summary>
    /// Id заметки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Заголовок заметки
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Текст заметки
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Ярлыки заметки
    /// </summary>
    public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();
}