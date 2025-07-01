using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionnaireController : ControllerBase
{
    private readonly IQuestionnaireService _service;

    public QuestionnaireController(IQuestionnaireService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(QuestionnaireCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
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
}
