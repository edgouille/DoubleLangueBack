using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Infrastructure.Interface.Utils;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Domain.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoubleLangue.Domain.Dto.User;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthController(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration configuration, IUserService userService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto login)
    {
        var user = await _userRepository.GetUserByEmailAsync(login.Email);
        if (user == null || !_passwordHasher.Verify(login.Password, user.Password))
        {
            return Unauthorized();
        }

        user.LastActivity = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName), 
            new Claim(ClaimTypes.Role, user.Role.ToString()) 
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        try
        {
            var createDto = new UserCreateDto
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                Role = UserRoleEnum.User
            };

            var user = await _userService.CreateAsync(createDto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
