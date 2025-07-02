using Microsoft.AspNetCore.Mvc;
using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DoubleLangue.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using DoubleLangue.Domain.Dto.User;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser(UserCreateDto request)
    {
        try
        {
            var user = await _userService.CreateAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var listUser = await _userService.GetAllAsync();
            return Ok(listUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return BadRequest("Invalid GUID format.");
        if (string.IsNullOrEmpty(id)) 
            return BadRequest("L'ID de l'utilisateur ne peut pas être un GUID vide.");

        try
        {
            var item = await _userService.GetByIdAsync(guid);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto userDto)
    {
        if (!Guid.TryParse(id, out var guid))
            return BadRequest("Invalid GUID format.");

        try
        {
            var updated = await _userService.UpdateAsync(guid, userDto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        try
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UserUpdateDto userDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        // Prevent role change by regular users
        userDto.Role = null;

        try
        {
            var updated = await _userService.UpdateAsync(userId, userDto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return BadRequest("Invalid GUID format.");

        try
        {
            await _userService.DeleteAsync(guid);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
