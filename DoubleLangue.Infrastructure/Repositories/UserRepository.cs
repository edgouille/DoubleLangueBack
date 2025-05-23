using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Users.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la récupération de l'utilisateur par ID.", ex);
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la récupération de tous les utilisateurs.", ex);
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }


    public async Task<User> AddAsync(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("could not connect") || ex.Message.Contains("A network-related or instance-specific error"))
        {
            throw new InvalidOperationException("La base de données n'est pas disponible ou la connexion a échoué.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de l'ajout de l'utilisateur.", ex);
        }
    }

    public async Task UpdateAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la mise à jour de l'utilisateur.", ex);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la suppression de l'utilisateur.", ex);
        }
    }
}
