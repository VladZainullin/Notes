using Microsoft.EntityFrameworkCore;

namespace Notes.Core.Interafaces;

public interface IHistory
{
    public DateTime DateOfModification { get; set; }

    public EntityState EntityState { get; set; }
}