using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Core.Entities;

namespace Notes.Data.Contexts;

public sealed class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) :
        base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Note> Notes { get; set; } = null!;

    public DbSet<Label> Labels { get; set; } = null!;

    public DbSet<NoteLabel> NoteLabels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_configuration.GetConnectionString("Postgres"), o => { o.MigrationsAssembly("Notes.Web"); });

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Notes.Data"));

        base.OnModelCreating(modelBuilder);
    }
}