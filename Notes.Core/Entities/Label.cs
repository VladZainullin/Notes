using Notes.Core.Interfaces;

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
    ///     История изменения ярлыка
    /// </summary>
    public IReadOnlyCollection<LabelHistory> Histories { get; } = new List<LabelHistory>();

    /// <summary>
    ///     Метод получения истории ярлыка
    /// </summary>
    /// <param name="visitor">Посетитель для ведения истории</param>
    /// <returns>История ярлыка</returns>
    public LabelHistory Access(IHasHistoryVisitor visitor)
    {
        return visitor.Visit(this);
    }
}