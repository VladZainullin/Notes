using Microsoft.EntityFrameworkCore;

namespace Notes.Core.Interfaces;

/// <summary>
/// Интерфейс истории для сущности
/// </summary>
public interface IHistory
{
    /// <summary>
    /// Дата внесения изменения
    /// </summary>
    public DateTime DateOfModification { get; set; }

    /// <summary>
    /// Состояние сущности
    /// </summary>
    public EntityState State { get; set; }
}