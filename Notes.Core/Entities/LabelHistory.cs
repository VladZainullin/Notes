using Microsoft.EntityFrameworkCore;
using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

/// <summary>
/// История ярлыка
/// </summary>
public sealed class LabelHistory : 
    IHistory
{
    /// <summary>
    /// Id истории ярлыка
    /// </summary>
    public int Id { get; set; }
   
    /// <summary>
    /// Наименование ярлыка, занесённое в историю
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Id ярлыка
    /// </summary>
    public int LabelId { get; set; }
    /// <summary>
    /// Ярлык
    /// </summary>
    public Label? Label { get; set; }
    
    /// <summary>
    /// Дата внесения изменения
    /// </summary>
    public DateTime DateOfModification { get; set; }
    
    /// <summary>
    /// Состояние заметки
    /// </summary>
    public EntityState State { get; set; }
}