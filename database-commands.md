# 📦 Entity Framework Core Database Commands (DoubleLangue Project)

This guide covers common EF Core CLI commands for working with migrations and the database in the **DoubleLangue** project.

## 🏗️ Project Structure Reference

- **Startup Project:** `DoubleLangue.Api`
- **DbContext Location:** `DoubleLangue.Infrastructure/AppDbContext.cs`
- **Solution Root:** `DoubleLangueBack.sln`

## 📌 Prerequisites

Ensure these packages are installed in your projects:

### In `DoubleLangue.Infrastructure.csproj`
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

### In `DoubleLangue.Api.csproj`
(Only if migrations are executed from this startup project)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 📤 Create a Migration

```bash
dotnet ef migrations add <MigrationName> --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

**Example:**
```bash
dotnet ef migrations add InitialMigration --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

---

## 📥 Apply Migrations to the Database

```bash
dotnet ef database update --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

---

## 🗑️ Remove the Last Migration

> Only if it hasn’t been applied to the database yet:

```bash
dotnet ef migrations remove --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

---

## 📃 List All Migrations

```bash
dotnet ef migrations list --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

---

## 🔍 Check the Generated SQL Script

```bash
dotnet ef migrations script --project DoubleLangue.Infrastructure --startup-project DoubleLangue.Api
```

---

## 🔧 Troubleshooting

- ❗**Avoid dynamic values in `HasData()`**:
  - Replace `Guid.NewGuid()` and `DateTime.Now` with **hardcoded values** or `DateTime.UtcNow` equivalents with `DateTimeKind.Utc`.

---

## 🧼 Clean Build Tips

If you experience migration issues:
```bash
dotnet clean
dotnet build
```

---

> 💡 These commands must be executed from the solution root (where `DoubleLangueBack.sln` is located).
