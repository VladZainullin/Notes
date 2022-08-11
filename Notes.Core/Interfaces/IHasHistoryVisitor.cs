using Notes.Core.Entities;

namespace Notes.Core.Interfaces;

/// <summary>
/// Интерфейс посетителя для ведения истории
/// </summary>
public interface IHasHistoryVisitor
{
    /// <summary>
    /// Метод создания истории для сущности ярлыка
    /// </summary>
    /// <param name="entity">Ярлык</param>
    /// <returns>История ярлыка</returns>
    LabelHistory Visit(Label entity);
    
    /// <summary>
    /// Метод создания истории для сущности заметки
    /// </summary>
    /// <param name="entity">Заметка</param>
    /// <returns>История заметки</returns>
    NoteHistory Visit(Note entity);
}