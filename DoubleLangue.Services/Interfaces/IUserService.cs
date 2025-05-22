using DoubleLangue.Domain;
using DoubleLangue.Domain.Dto;

namespace DoubleLangue.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    //Task CreateAsync(UserDto user);
    Task<User> CreateUserAsync(string userName, string email, string password);
    Task UpdateAsync(UserDto user);
    Task DeleteAsync(Guid id);
}