# Blazor + PostgreSQL Starter

**A production-ready Blazor Server + PostgreSQL template optimized for Railway deployment.**

Build full-stack web applications with C# and .NET 9. This template includes Entity Framework Core, automatic database migrations, and a complete CRUD demo ready to extend.

[![Deploy on Railway](https://railway.app/button.svg)](https://railway.com/deploy/blazor-postgresql--1)

## ✨ Features

- 🚀 **One-Click Deploy** - Blazor app + PostgreSQL database
- ⚡ **Blazor Server** - Build interactive UIs with C#
- 🗄️ **PostgreSQL** - Production database included
- 🔄 **Auto Migrations** - Database schema updates automatically
- 🎯 **Entity Framework Core** - Modern ORM for .NET
- 📝 **CRUD Demo** - Working example with create, read, update, delete
- 🔧 **Production Ready** - Health checks, error handling, Docker optimized
- 🌐 **.NET 9** - Latest .NET features and performance

## 🚀 Quick Start

### Deploy to Railway

Click the "Deploy on Railway" button above, then:

1. **Add PostgreSQL Database:**
   - After the app deploys, click **"+ New"** → **"Database"** → **"Add PostgreSQL"**
   - The `DATABASE_URL` variable will automatically be added to your app

2. **Redeploy:**
   - Your app will automatically redeploy with the database connected
   - Migrations run automatically on startup

3. **Visit your app:**
   - Open the generated Railway URL
   - Go to `/database` to see the CRUD demo!

**✨ First deployment may show an error until PostgreSQL is added - this is expected!**

### Local Development
```bash
# Clone the repository
git clone https://github.com/YOUR_USERNAME/blazor-postgres-starter.git
cd blazor-postgres-starter/BlazorPostgresStarter

# Install PostgreSQL locally or use Docker
docker run --name postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres

# Update connection string in appsettings.json if needed

# Run the application
dotnet run

# Open browser to https://localhost:5001
# Visit /database to test the database connection
```

## 📁 Project Structure
```
blazor-postgres-starter/
├── BlazorPostgresStarter/          # Main application
│   ├── Components/                 # Blazor components
│   │   ├── Layout/                # Layout components
│   │   ├── Pages/                 # Page components
│   │   │   └── Database.razor     # CRUD demo page ⭐
│   │   └── App.razor              # Root component
│   ├── Data/                      # Database context and models
│   │   └── AppDbContext.cs        # EF Core DbContext
│   ├── Services/                  # Business logic
│   │   └── SampleItemService.cs   # Database operations
│   ├── Migrations/                # Database migrations
│   ├── wwwroot/                   # Static files
│   ├── Program.cs                 # App configuration
│   └── appsettings.json           # Configuration
├── Dockerfile                      # Multi-stage Docker build
├── railway.toml                    # Railway configuration
└── README.md                       # Documentation
```

## 🗄️ Database Demo

The template includes a **working CRUD example** at `/database`:

**Features:**
- ✅ View all items from database
- ✅ Add new items
- ✅ Delete items
- ✅ Real-time UI updates
- ✅ Type-safe LINQ queries

**Sample Model:**
```csharp
public class SampleItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## 🛠️ Add Your Own Models

### 1. Create a New Model

Create a new class in the `Data` folder:
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
```

### 2. Add to DbContext

Update `AppDbContext.cs`:
```csharp
public DbSet<Product> Products { get; set; }
```

### 3. Create Migration
```bash
dotnet ef migrations add AddProducts
```

### 4. Deploy
```bash
git add .
git commit -m "Add Products model"
git push
```

Railway automatically runs the migration on deploy! 🎉

## ⚙️ Environment Variables

Railway automatically sets:
- `DATABASE_URL` - PostgreSQL connection string (auto-configured)
- `PORT` - Application port
- `ASPNETCORE_ENVIRONMENT` - Set to Production

No manual configuration needed!

## 🎯 Common Use Cases

📊 **Data-Driven Dashboards** - Real-time analytics with live database updates  
🛒 **E-Commerce Apps** - Product catalogs, inventory, orders  
👥 **CRM Systems** - Customer and contact management  
📝 **Content Management** - Blogs, wikis, documentation sites  
🎫 **Ticketing Systems** - Support tickets, issue tracking  
💼 **Business Apps** - Inventory, invoicing, HR management

## 📚 Extend the Template

### Add Authentication
```bash
cd BlazorPostgresStarter
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Update `AppDbContext` to inherit from `IdentityDbContext`:
```csharp
public class AppDbContext : IdentityDbContext
{
    // ... your DbSets
}
```

### Add API Endpoints

Mix Blazor UI with REST APIs:
```csharp
app.MapGet("/api/items", async (AppDbContext db) =>
    await db.SampleItems.ToListAsync());

app.MapPost("/api/items", async (SampleItem item, AppDbContext db) =>
{
    db.SampleItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/items/{item.Id}", item);
});
```

### Add File Storage
```bash
# Add Railway volume or cloud storage
# Store uploaded files alongside your data
```

## 🔍 Database Tools

### View Data in Railway

1. Go to your Railway project
2. Click on PostgreSQL service
3. Click "Data" tab to browse tables

### Connect with pgAdmin or psql

Get the `DATABASE_URL` from Railway dashboard and use your favorite PostgreSQL client.

## 📚 Learn More

- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL on Railway](https://docs.railway.app/databases/postgresql)
- [Railway Documentation](https://docs.railway.app/)

## 🤝 Contributing

Contributions welcome! Please submit a Pull Request.

## 📄 License

MIT License - see LICENSE file

---

**Built for the Railway community** 🚂

**⭐ Pro Tip:** Visit `/database` after deployment to see the PostgreSQL integration in action!
