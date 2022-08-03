namespace Notes.Core.Entities;

/// <summary>
/// Ярлык
/// </summary>
public sealed class Label
{
    /// <summary>
    /// Id ярлыка
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование ярлыка
    /// </summary>
    public int Title { get; set; }

    public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();
}