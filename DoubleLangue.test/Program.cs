using System;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Entrez la chaîne de connexion :");
            string? connectionString = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine("Chaîne de connexion invalide.");
                return;
            }

            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var context = new TestDbContext(optionsBuilder.Options))
            {
                // Utilisez le contexte ici, par exemple :
                // var count = context.MyEntities.Count();
                Console.WriteLine("Connexion à la base de données réussie.");
            }
        }
    }

    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        // Ajoutez vos DbSet ici, par exemple :
        // public DbSet<MyEntity> MyEntities { get; set; }
    }
}