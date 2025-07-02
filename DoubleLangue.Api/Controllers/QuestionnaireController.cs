using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Domain.Dto.Answer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class QuestionnaireController : ControllerBase
{
    private readonly IQuestionnaireService _service;
    private readonly IAnswerService _answerService;
    private readonly IUserService _userService;

    public QuestionnaireController(IQuestionnaireService service, IAnswerService answerService, IUserService userService)
    {
        _service = service;
        _answerService = answerService;
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(QuestionnaireCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQuestionnaire([FromQuery] int level = 1, [FromQuery] MathProblemType? type = null, [FromQuery] bool test = true)
    {
        var result = await _service.GenerateAsync(level, type, test);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost("{id}/question")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddQuestion(Guid id, AddQuestionToQuestionnaireDto dto)
    {
        try
        {
            await _service.AddQuestionAsync(id, dto.QuestionId, dto.OrderInQuiz);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("evaluate")]
    public async Task<IActionResult> EvaluateQuestionnaire([FromBody] QuestionnaireCheckDto dto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            foreach (var answer in dto.Answers)
            {
                var createDto = new AnswerCreateDto
                {
                    UserId = userId,
                    QuestionId = answer.QuestionId,
                    UserAnswer = answer.Answer
                };
                await _answerService.SaveAsync(createDto);
            }

            var score = await _service.CheckAnswersAsync(dto.QuestionnaireId, dto.Answers);

            _userService.UpdateAsync(userId, )
            return Ok(new { score });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuestionnaireByAdmin([FromBody] QuestionnaireAdminCreateDto dto)
    {
        try
        {
            var createDto = new QuestionnaireCreateDto
            {
                Title = dto.Title,
                Description = dto.Description,
                ExamDateTime = dto.ExamDateTime
            };
            var created = await _service.CreateAsync(createDto);

            var order = 0;
            foreach (var q in dto.QuestionsList)
            {
                await _service.AddQuestionAsync(created.Id, q, order++);
            }

            var result = await _service.GetByIdAsync(created.Id);
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
