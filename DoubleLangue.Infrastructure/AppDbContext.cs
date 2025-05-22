using DoubleLangue.Domain;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    //    modelBuilder.Entity<User>()
    //        .HasData(new User
    //        {
    //            Id = adminId,
    //            UserName = "edgarAdmin",
    //            Email = "edgar@andre.com",
    //            Password = "123",
    //            Role = "Admin", // À refactor en enum plus tard
    //            CreatedAt = new DateTime(2025, 5, 22, 0, 0, 0, DateTimeKind.Utc)
    //        });

    //    base.OnModelCreating(modelBuilder);
    //}

    public DbSet<User> Users => Set<User>();
}