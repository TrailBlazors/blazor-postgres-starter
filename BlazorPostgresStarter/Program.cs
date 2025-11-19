using BlazorPostgresStarter.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Railway PORT - Critical for Railway deployment
// Only override port if PORT environment variable is set (Railway)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add PostgreSQL Database
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(connectionString))
{
    // Railway provides DATABASE_URL, convert to EF Core format
    Console.WriteLine("Using Railway DATABASE_URL");
    connectionString = ConvertToEFConnectionString(connectionString);
    builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // Fallback for local development
    Console.WriteLine("Using local connection string");
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        Console.WriteLine("WARNING: No database connection string found!");
    }
    builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(connectionString ?? ""));
}

// Add the sample service
builder.Services.AddScoped<BlazorPostgresStarter.Services.SampleItemService>();

// Add health checks for Railway monitoring
builder.Services.AddHealthChecks();

var app = builder.Build();

try
{
    Console.WriteLine("Starting database migration...");
    using (var scope = app.Services.CreateScope())
    {
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var db = await dbContextFactory.CreateDbContextAsync();
        db.Database.Migrate();
        Console.WriteLine("Database migration completed successfully");
    }
}
catch (Exception ex)
{
    // Log but don't crash - allows health check to pass
    Console.WriteLine($"Migration error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

// Health check endpoint for Railway
app.MapHealthChecks("/health");

app.MapRazorComponents<BlazorPostgresStarter.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();

// Helper method to convert Railway DATABASE_URL to EF Core format
static string ConvertToEFConnectionString(string databaseUrl)
{
    try
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        var connString = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.Trim('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
        Console.WriteLine($"Converted connection string: Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.Trim('/')};Username={userInfo[0]}");
        return connString;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error converting DATABASE_URL: {ex.Message}");
        throw;
    }
}