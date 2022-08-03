using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Core.Entities;

namespace Notes.Data.Contexts;

public sealed class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Note> Note { get; set; } = null!;
    
    public DbSet<Label> Labels { get; set; } = null!;
    
    public DbSet<NoteLabel> NoteLabels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(
            _configuration.GetConnectionString("Postgres"),
            options =>
        {
            options.MigrationsAssembly("Notes.Web");
        });

        base.OnConfiguring(builder);
    }
}