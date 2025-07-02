using DoubleLangue.Domain.Dto.User;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Infrastructure.Interface.Utils;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponseDto> CreateAsync(UserCreateDto user)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new Exception("Cet email est déjà utilisé.");
        }

        var existingUserName = await _userRepository.GetUserByUserNameAsync(user.UserName);
        if (existingUserName != null)
        {
            throw new Exception("Ce nom d'utilisateur est déjà utilisé.");
        }

        var hashedPassword = _passwordHasher.Hash(user.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = user.UserName,
            Email = user.Email,
            Password = hashedPassword,
            Role = user.Role,
            CreatedAt = DateTime.UtcNow,
            LastActivity = DateTime.UtcNow
        };

        await _userRepository.AddAsync(newUser);

        var userResponse = await _userRepository.GetUserByEmailAsync(user.Email);

        if (userResponse == null)
        {
            throw new Exception("Erreur lors de la création");
        }

        return new UserResponseDto
        {
            Id = userResponse.Id.ToString(),
            UserName = userResponse.UserName,
            Email = userResponse.Email,
            Role = userResponse.Role,
            LastActivity = userResponse.LastActivity
        };
    }

    public async Task<List<User>> GetAllAsync()
    {
        try
        {
            return await _userRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            throw new Exception("Erreur lors de la récupération de tous les utilisateurs.", ex);
        }
    }
    //TODO: have to be different beet ween user and admin

    public async Task<User?> GetByIdAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.LastActivity = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
            }
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la récupération de l'utilisateurs ayant l'id = {id.ToString()}.", ex);
        }
    }
    //TODO: have to be different beet ween user and admin

    public async Task<UserResponseDto?> UpdateAsync(Guid id, UserUpdateDto user)
    {
        try
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser is null)
            {
                return null;
            }

            if (user.UserName != null && user.UserName != existingUser.UserName)
            {
                var checkName = await _userRepository.GetUserByUserNameAsync(user.UserName);
                if (checkName != null)
                {
                    throw new Exception("Ce nom d'utilisateur est déjà utilisé.");
                }
            }

            if (user.Email != null && user.Email != existingUser.Email)
            {
                var checkEmail = await _userRepository.GetUserByEmailAsync(user.Email);
                if (checkEmail != null)
                {
                    throw new Exception("Cet email est déjà utilisé.");
                }
            }

            var updatedUser = new User
            {
                Id = existingUser.Id,
                UserName = user.UserName ?? existingUser.UserName,
                Email = user.Email ?? existingUser.Email,
                Password = user.Password != null ? _passwordHasher.Hash(user.Password) : existingUser.Password,
                Role = user.Role ?? existingUser.Role,
                CreatedAt = existingUser.CreatedAt,
                LastActivity = DateTime.UtcNow
            };

            var result = await _userRepository.UpdateAsync(updatedUser);

            return new UserResponseDto
            {
                Id = result!.Id.ToString(),
                UserName = result.UserName,
                Email = result.Email,
                Role = result.Role,
                LastActivity = result.LastActivity
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la mise à jour de l'utilisateur", ex);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Erreur lors de la suppression", ex);
        }
    }

    public async Task<List<UserLeaderboardDto>> GetLeaderboardAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users
            .OrderByDescending(u => u.Score)
            .Select(u => new UserLeaderboardDto
            {
                Id = u.Id.ToString(),
                UserName = u.UserName,
                Score = u.Score
            })
            .ToList();
    }
}