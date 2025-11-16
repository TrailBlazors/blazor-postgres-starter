# Blazor + PostgreSQL Starter

**A production-ready Blazor Server + PostgreSQL template optimized for Railway deployment.**

Build full-stack web applications with C# and .NET 9. This template includes Entity Framework Core, automatic database migrations, and a complete CRUD setup ready to extend.

[![Deploy on Railway](https://railway.app/button.svg)](https://railway.app/template/TEMPLATE_ID)

## ✨ Features

- 🚀 **One-Click Deploy** - Blazor app + PostgreSQL database
- ⚡ **Blazor Server** - Build interactive UIs with C#
- 🗄️ **PostgreSQL** - Production database included
- 🔄 **Auto Migrations** - Database schema updates automatically
- 🎯 **Entity Framework Core** - Modern ORM for .NET
- 🔧 **Production Ready** - Health checks, error handling, Docker optimized
- 🌐 **.NET 9** - Latest .NET features and performance

## 🚀 Quick Start

### Deploy to Railway

Click the "Deploy on Railway" button above. Railway will automatically:
- Deploy the Blazor application
- Provision a PostgreSQL database
- Connect them together
- Run database migrations
- Generate a public URL

### Local Development
```bash
# Clone the repository
git clone https://github.com/YOUR_USERNAME/blazor-postgres-starter.git
cd blazor-postgres-starter/BlazorPostgresStarter

# Install PostgreSQL locally or use Docker
docker run --name postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres

# Update connection string in appsettings.json if needed

# Run migrations
dotnet ef database update

# Run the application
dotnet run

# Open browser to https://localhost:5001
```

## 📁 Project Structure
```
blazor-postgres-starter/
├── BlazorPostgresStarter/          # Main application
│   ├── Components/                 # Blazor components
│   │   ├── Layout/                # Layout components
│   │   ├── Pages/                 # Page components
│   │   └── App.razor              # Root component
│   ├── Data/                      # Database context and models
│   │   └── AppDbContext.cs        # EF Core DbContext
│   ├── Migrations/                # Database migrations
│   ├── wwwroot/                   # Static files
│   ├── Program.cs                 # App configuration
│   └── appsettings.json           # Configuration
├── Dockerfile                      # Multi-stage Docker build
├── railway.toml                    # Railway configuration
└── README.md                       # Documentation
```

## 🗄️ Database Setup

### Included Example Model
```csharp
public class SampleItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### Add Your Own Models

1. Create a new class in the `Data` folder:
```csharp
public class YourModel
{
    public int Id { get; set; }
    public string PropertyName { get; set; }
}
```

2. Add it to `AppDbContext.cs`:
```csharp
public DbSet<YourModel> YourModels { get; set; }
```

3. Create and apply migration:
```bash
dotnet ef migrations add AddYourModel
dotnet ef database update
```

4. Push to GitHub - Railway auto-deploys and runs migrations!

## ⚙️ Environment Variables

Railway automatically sets:
- `DATABASE_URL` - PostgreSQL connection string
- `PORT` - Application port
- `ASPNETCORE_ENVIRONMENT` - Set to Production

### Optional Variables

Add these in Railway if needed:
- `ASPNETCORE_URLS` - Already configured via PORT
- Custom app settings via `appsettings.json`

## 🛠️ Common Tasks

### View Database Data

Use Railway's PostgreSQL plugin UI or connect with:
```bash
# Get DATABASE_URL from Railway dashboard
psql $DATABASE_URL
```

### Add Authentication
```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Then update `AppDbContext` to inherit from `IdentityDbContext`.

### Add More Entity Framework Packages
```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
```

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