namespace Notes.Core.Entities;

/// <summary>
/// Заметка
/// </summary>
public class Note
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
    /// Тело заметки
    /// </summary>
    public string? Body { get; set; }
}