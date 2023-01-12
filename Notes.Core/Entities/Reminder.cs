namespace Notes.Core.Entities;

/// <summary>
///     Напоминание
/// </summary>
public sealed class Reminder
{
    /// <summary>
    ///     Id напоминания
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Дата включения
    /// </summary>
    public TimeOnly TimeOfInclusion { get; set; }

    /// <summary>
    ///     Id заметки
    /// </summary>
    public int NoteId { get; set; }

    /// <summary>
    ///     Заметка
    /// </summary>
    public Note? Note { get; set; }
}