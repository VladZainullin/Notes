using Microsoft.EntityFrameworkCore;
using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

public sealed class LabelHistory : 
    IHistory
{
    public int Id { get; set; }
    
    public string? Title { get; set; }

    public int LabelId { get; set; }
    public Label? Label { get; set; }
    
    public DateTime DateOfModification { get; set; }
    public EntityState State { get; set; }
}