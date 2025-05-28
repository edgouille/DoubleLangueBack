using DoubleLangue.Domain.Dto;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> CreateAsync(UserCreateDto user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    //Task<User> UpdateAsync(UserDto user);
    Task DeleteAsync(Guid id);
}