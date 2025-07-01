using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class QuestionnaireController : ControllerBase
{
    private readonly IQuestionnaireService _service;

    public QuestionnaireController(IQuestionnaireService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(QuestionnaireCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQuestionnaire([FromQuery] int level = 1)
    {
        var result = await _service.GenerateAsync(level);
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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    //[HttpPut]
    //public async Task<IActionResult> CreateQuestionnaireByAdmin(title, description, exemaDate, level, QuestionsList)
    //{

    //}
}
