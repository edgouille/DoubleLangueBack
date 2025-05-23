using DoubleLangue.Domain.Dto;
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

    public async Task<List<User>> GetAllAsync()
    {
        return new List<User>();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(UserDto user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(user.UserName))
            throw new ArgumentException("UserName ne peut pas être null ou vide.", nameof(user.UserName));
        if (string.IsNullOrEmpty(user.Email))
            throw new ArgumentException("Email ne peut pas être null ou vide.", nameof(user.Email));
        if (!Enum.IsDefined(typeof(UserRoleEnum), user.Role))
            throw new ArgumentException("Role n'est pas valide.", nameof(user.Role));
        if (string.IsNullOrEmpty(user.Password))
            throw new ArgumentException("Password ne peut pas être null ou vide.", nameof(user.Password));

        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            // Tu peux lancer une exception ou retourner null selon ta gestion d'erreurs
            throw new Exception("Cet email est déjà utilisé.");
        }

        var hashedPassword = _passwordHasher.Hash(user.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = user.UserName,
            Email = user.Email,
            Password = hashedPassword,
            Role = user.Role,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(newUser);
        return newUser;
    }

    public async Task UpdateAsync(UserDto user)
    {
        return;
    }

    public async Task DeleteAsync(Guid id)
    {
        return;
    }


}