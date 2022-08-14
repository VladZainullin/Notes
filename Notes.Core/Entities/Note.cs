using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

/// <summary>
///     Заметка
/// </summary>
public sealed class Note :
    IHasHistory<NoteHistory>
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
    ///     Флаг закреплена/откреплена
    /// </summary>
    public bool IsPinned { get; set; }

    /// <summary>
    ///     Id пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     Пользователь
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    ///     Ярлыки заметок
    /// </summary>
    public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();

    /// <summary>
    ///     История заметки
    /// </summary>
    public IReadOnlyCollection<NoteHistory> Histories { get; } = new List<NoteHistory>();

    /// <summary>
    ///     Метод получения истории заметки
    /// </summary>
    /// <param name="visitor">Посетитель для ведения истории</param>
    /// <returns>История заметки</returns>
    public NoteHistory Access(IHasHistoryVisitor visitor)
    {
        return visitor.Visit(this);
    }
}