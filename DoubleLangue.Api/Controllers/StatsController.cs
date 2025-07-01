using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IQuestionnaireService _questionnaireService;
    private readonly IQuestionService _questionService;

    public StatsController(IUserService userService, IQuestionnaireService questionnaireService, IQuestionService questionService)
    {
        _userService = userService;
        _questionnaireService = questionnaireService;
        _questionService = questionService;
    }

    [HttpGet("allStats")]
    public async Task<IActionResult> GetAllStats()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            var questionnaires = await _questionnaireService.GetAllAsync();
            var questions = await _questionService.GetAllAsync();
            var userOnlineLast3days = users.Count(u => u.LastActivity >= DateTime.UtcNow.AddDays(-3));
            var averageScore = users.Average(u => u.Score);
            var medianScore = users
                .Select(u => u.Score)
                .OrderBy(score => score)
                .Skip(users.Count / 2)
                .FirstOrDefault();

            return Ok(new
            {
                UserCount = users.Count,
                UserOnlineLast3days = userOnlineLast3days,
                QuestionnaireCount = questionnaires.Count,
                QuestionCount = questions.Count,
                AverageScore = averageScore,
                MedianScore = medianScore
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
