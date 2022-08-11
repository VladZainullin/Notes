using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notes.Core.Entities;
using Notes.Core.Interfaces;
using Notes.Data.Visitors;

namespace Notes.Data.Contexts;

/// <summary>
/// Пользовательский контекст базы данных
/// </summary>
public sealed class AppDbContext : DbContext
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор пользовательского контекста базы данных
    /// </summary>
    /// <param name="options">Опции контекста</param>
    /// <param name="configuration">Конфигурация приложения</param>
    public AppDbContext(
        DbContextOptions<AppDbContext> options, 
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Заметки
    /// </summary>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <summary>
    /// Ярлыки
    /// </summary>
    public DbSet<Label> Labels { get; set; } = null!;

    /// <summary>
    /// Ярлыки заметок
    /// </summary>
    public DbSet<NoteLabel> NoteLabels { get; set; } = null!;

    /// <summary>
    /// Переопределённый метод конфигурации контекста базы данных
    /// </summary>
    /// <param name="builder">Строитель контекста базы данных</param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_configuration.GetConnectionString("Postgres"), o => { o.MigrationsAssembly("Notes.Web"); });

        base.OnConfiguring(builder);
    }

    /// <summary>
    /// Переопределённый метод моделирования сущностей контекста
    /// </summary>
    /// <param name="modelBuilder">Строитель модели сущностей контекста</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Notes.Data"));

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Переопределённый метод сохранения данных контекста базы данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность сохранения</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var histories = ChangeTracker
            .Entries<IHasHistory<IHistory>>()
            .Select(h => h
                .Entity
                .Access(new HasHistoryVisitor(h.State)));
        
        await AddRangeAsync(histories, cancellationToken);
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}