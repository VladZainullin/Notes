namespace Notes.Core.Interfaces;

/// <summary>
///     Интерфейс наличия истории у сущности
/// </summary>
/// <typeparam name="THistory"></typeparam>
public interface IHasHistory<out THistory>
    where THistory : IHistory
{
    /// <summary>
    ///     История изменения сущности
    /// </summary>
    IReadOnlyCollection<THistory> Histories { get; }

    /// <summary>
    ///     Метод создания слепка сущности для ведения истории
    /// </summary>
    /// <param name="visitor">Посетитель создания истории</param>
    /// <returns>Сущность истории</returns>
    THistory Access(IHasHistoryVisitor visitor);
}