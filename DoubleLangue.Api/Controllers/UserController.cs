using Microsoft.AspNetCore.Mvc;
using DoubleLangue.Domain.Dto;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Domain.Models;

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

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto request)
    {
        User user;
        try
        {
            user = await _userService.CreateAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return BadRequest("Invalid GUID format.");

        var item = await _userService.GetByIdAsync(guid);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
    {
        // TODO: Ajouter la logique de mise à jour d'un utilisateur
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id, string token)
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
