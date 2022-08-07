using Microsoft.EntityFrameworkCore;
using Notes.Core.Interfaces;

namespace Notes.Core.Entities;

public sealed class NoteHistory : IHistory
{
    public int Id { get; set; }
    public string? Header { get; set; }
    public string? Body { get; set; }
    
    public DateTime DateOfModification { get; set; }
    public EntityState State { get; set; }

    public int NoteId { get; set; }
    public Note? Note { get; set; }
}