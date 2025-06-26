using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DoubleLangue.Infrastructure;

public static class DataSeeder
{
    public static async Task SeedAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        await context.Database.EnsureCreatedAsync();

        var adminConfig = configuration.GetSection("AdminSeed");
        var userName = adminConfig["UserName"];
        var email = adminConfig["Email"];
        var password = adminConfig["Password"];
        var id = adminConfig["Id"];

        if (string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        if (!await context.Users.AnyAsync(u => u.UserName == userName))
        {
            var admin = new User
            {
                Id = !string.IsNullOrWhiteSpace(id) ? Guid.Parse(id) : Guid.NewGuid(),
                UserName = userName!,
                Email = email!,
                Password = hasher.Hash(password!),
                Role = UserRoleEnum.Admin,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}