using Microsoft.EntityFrameworkCore;
using Notes.Core.Interfaces;

namespace Notes.Data.Extensions;

public static class DbContextExtensions
{
    public static async Task<bool> IsExistsAsync<T>(
        this DbContext context,
        int id,
        CancellationToken cancellationToken) where T : class, IHasId
    {
        var exists = await context
            .Set<T>()
            .AnyAsync(e => e.Id == id, cancellationToken);

        return exists;
    }
    
    public static async Task<T> GetAsync<T>(
        this DbContext context,
        int id,
        CancellationToken cancellationToken) where T : class, IHasId
    {
        var exists = await context
            .Set<T>()
            .SingleAsync(e => e.Id == id, cancellationToken);

        return exists;
    }
}