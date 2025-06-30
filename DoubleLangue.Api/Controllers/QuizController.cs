using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoubleLangue.Domain.Dto.Quiz;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromQuery] string userId, [FromQuery] int level = 1, [FromQuery] MathProblemType type = MathProblemType.Result)
    {
        if (!Guid.TryParse(userId, out var uid))
        {
            return BadRequest("Invalid user id");
        }
        var quiz = await _quizService.CreateAsync(uid, level, type);
        return Ok(quiz);
    }

    [HttpPost("{questionId}/answer")]
    [AllowAnonymous]
    public async Task<IActionResult> Answer(string questionId, [FromBody] AnswerDto dto)
    {
        if (!Guid.TryParse(questionId, out var qid))
            return BadRequest("Invalid question id");

        var result = await _quizService.SaveAnswerAsync(qid, dto.Answer);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
