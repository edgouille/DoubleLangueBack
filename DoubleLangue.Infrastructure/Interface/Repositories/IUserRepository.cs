using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}

