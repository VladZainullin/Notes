using Microsoft.EntityFrameworkCore;
using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

/// <summary>
/// История заметки
/// </summary>
public sealed class NoteHistory : 
    IHistory
{
    /// <summary>
    /// Id истории заметки
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Заголовок заметки, занесённый в историю
    /// </summary>
    public string? Header { get; set; }
    
    /// <summary>
    /// Тело заметки, занесённое в историю
    /// </summary>
    public string? Body { get; set; }
    
    /// <summary>
    /// Дата внесения изменения 
    /// </summary>
    public DateTime DateOfModification { get; set; }
    
    /// <summary>
    /// Состояние заметки
    /// </summary>
    public EntityState State { get; set; }
    
    /// <summary>
    /// Id заметки
    /// </summary>
    public int NoteId { get; set; }
    /// <summary>
    /// Заметка
    /// </summary>
    public Note? Note { get; set; }
}