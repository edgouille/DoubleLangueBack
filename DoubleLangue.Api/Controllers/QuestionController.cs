using DoubleLangue.Domain.Dto.Question;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _service;

    public QuestionController(IQuestionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(QuestionCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQuestion([FromQuery] int level = 1, [FromQuery] MathProblemType type = MathProblemType.Result)
    {
        var result = await _service.GenerateAsync(level, type);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }
}
