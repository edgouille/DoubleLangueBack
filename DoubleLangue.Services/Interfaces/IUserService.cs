using DoubleLangue.Domain.Dto.User;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> CreateAsync(UserCreateDto user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<UserResponseDto?> UpdateAsync(Guid id, UserUpdateDto user);
    Task DeleteAsync(Guid id);
    Task<List<UserLeaderboardDto>> GetLeaderboardAsync();
    Task IncreaseScoreAsync(Guid userId, int scoreDelta);
}