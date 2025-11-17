using BlazorPostgresStarter.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorPostgresStarter.Services;

public class SampleItemService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public SampleItemService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<SampleItem>> GetAllItemsAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.SampleItems.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<SampleItem> AddItemAsync(string name)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var item = new SampleItem { Name = name, CreatedAt = DateTime.UtcNow };
        context.SampleItems.Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteItemAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var item = await context.SampleItems.FindAsync(id);
        if (item != null)
        {
            context.SampleItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}