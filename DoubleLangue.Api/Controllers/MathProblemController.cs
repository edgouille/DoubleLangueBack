using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DoubleLangue.Services.Interfaces;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MathProblemController : ControllerBase
{
    private readonly IMathProblemGeneratorService _generatorService;

    public MathProblemController(IMathProblemGeneratorService generatorService)
    {
        _generatorService = generatorService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetProblem([FromQuery] int level = 1, [FromQuery] MathProblemType type = MathProblemType.Result)
    {
        try
        {
            var problem = _generatorService.Generate(level, type);
            return Ok(problem);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
