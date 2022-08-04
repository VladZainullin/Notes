namespace Notes.Core.Entities;

/// <summary>
/// Ярлыка заметки
/// </summary>
public sealed class NoteLabel
{
    /// <summary>
    /// Id заметки
    /// </summary>
    public int NoteId { get; set; }

    /// <summary>
    /// Заметка
    /// </summary>
    public Note? Note { get; set; }
    
    /// <summary>
    /// Id ярлыка
    /// </summary>
    public int LabelId { get; set; }

    /// <summary>
    /// Ярлык
    /// </summary>
    public Label? Label { get; set; }
}