using Microsoft.AspNetCore.Mvc;
using DoubleLangue.Domain.Dto;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Domain.Models;
using Microsoft.IdentityModel.Tokens;

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
    public async Task<IActionResult> CreateUser(UserCreateDto request)
    {
        UserResponseDto user;
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

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        List<User> listUser;
        try
        {
            listUser = await _userService.GetAllAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        return Ok(listUser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return BadRequest("Invalid GUID format.");
        if (id.IsNullOrEmpty())
            throw new ArgumentException("L'ID de l'utilisateur ne peut pas être un GUID vide.", nameof(id));

        var item = await _userService.GetByIdAsync(guid);

        if (item == null) return NotFound();
        return Ok(item);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
    //{


    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // TODO: Ajouter la logique de suppression d'un utilisateur
        return NoContent();
    }


}
