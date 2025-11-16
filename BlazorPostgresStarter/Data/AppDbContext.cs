using Microsoft.EntityFrameworkCore;

namespace BlazorPostgresStarter.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Example DbSet - you can add more models here
    public DbSet<SampleItem> SampleItems { get; set; }
}

// Example model
public class SampleItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}