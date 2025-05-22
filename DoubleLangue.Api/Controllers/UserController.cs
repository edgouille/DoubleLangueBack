using Microsoft.AspNetCore.Mvc;
using DoubleLangue.Domain.Dto;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //[HttpPost]
    //public IActionResult CreateUser([FromBody] UserDto userDto)
    //{
    //    // TODO: Ajouter la logique de création d'utilisateur

    //    _userService.CreateAsync(userDto);
    //    return Ok();
    //}

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var user = await _userService.CreateUserAsync(request.UserName, request.Email, request.Password);
        return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        // TODO: Ajouter la logique de récupération d'un utilisateur par ID
        return Ok(/* utilisateur */);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
    {
        // TODO: Ajouter la logique de mise à jour d'un utilisateur
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // TODO: Ajouter la logique de suppression d'un utilisateur
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        // TODO: Ajouter la logique de récupération de tous les utilisateurs
        return Ok(/* liste des utilisateurs */);
    }
}

public record CreateUserRequest(string UserName, string Email, string Password);