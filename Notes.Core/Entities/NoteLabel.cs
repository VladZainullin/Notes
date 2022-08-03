using System.Reflection.Emit;

namespace Notes.Core.Entities;

/// <summary>
/// Ярлык заметки
/// </summary>
public sealed class NoteLabel
{
    /// <summary>
    /// Id ярлыка заметки
    /// </summary>
    public int Id { get; set; }

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