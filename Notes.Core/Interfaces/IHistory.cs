using Microsoft.EntityFrameworkCore;

namespace Notes.Core.Interfaces;

public interface IHistory
{
    public DateTime DateOfModification { get; set; }

    public EntityState State { get; set; }
}