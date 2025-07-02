using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly IUserService _userService;

    public LeaderboardController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaderboard()
    {
        var leaderboard = await _userService.GetLeaderboardAsync();
        return Ok(leaderboard);
    }
}

