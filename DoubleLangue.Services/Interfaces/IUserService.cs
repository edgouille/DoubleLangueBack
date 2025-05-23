using DoubleLangue.Domain.Dto;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task<User> CreateAsync(UserDto user);
    Task UpdateAsync(UserDto user);
    Task DeleteAsync(Guid id);
}