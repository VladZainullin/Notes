using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Core.Entities;
using Notes.Core.Interafaces;
using Notes.Core.Interfaces;
using Notes.Data.Visitors;

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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<IHasHistory<IHistory>>()
            .ToList();
        foreach (var entry in entries)
        {
            var history = entry.Entity.Access(new HasHistoryVisitor(entry.State));
            await AddAsync(history, cancellationToken);
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}