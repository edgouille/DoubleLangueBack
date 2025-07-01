using DoubleLangue.Domain.Dto.Answer;
using DoubleLangue.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _service;

    public AnswerController(IAnswerService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AnswerCreateDto dto)
    {
        try
        {
            var result = await _service.SaveAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }
}
