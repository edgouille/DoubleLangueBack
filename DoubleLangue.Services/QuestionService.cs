using DoubleLangue.Domain.Dto.Question;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repository;
    private readonly IMathProblemGeneratorService _mathProblemGeneratorService;

    public QuestionService(IQuestionRepository repository, IMathProblemGeneratorService mathProblemGeneratorService)
    {
        _repository = repository;
        _mathProblemGeneratorService = mathProblemGeneratorService;
    }

    public async Task<QuestionResponseDto> CreateAsync(QuestionCreateDto dto)
    {
        var question = new Question
        {
            QuestionText = dto.QuestionText,
            CorrectAnswer = dto.CorrectAnswer,
            Difficulty = dto.Difficulty
        };
        await _repository.AddAsync(question);
        return new QuestionResponseDto
        {
            Id = Guid.NewGuid(),
            QuestionText = question.QuestionText,
            CorrectAnswer = question.CorrectAnswer,
            Difficulty = question.Difficulty
        };
    }

    public async Task<QuestionResponseDto> GenerateAsync(int level, MathProblemType type)
    {
       var question  = _mathProblemGeneratorService.Generate(level, type);
        if (question == null)
        {
            throw new InvalidOperationException("Failed to generate question.");
        }
        
        return new QuestionResponseDto
        {
            Id = Guid.NewGuid(),
            QuestionText = question.Question,
            CorrectAnswer = question.Answer,
            Difficulty = level
        };
    }

    public async Task<List<QuestionResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(q => new QuestionResponseDto
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            CorrectAnswer = q.CorrectAnswer,
            Difficulty = q.Difficulty
        }).ToList();
    }

    public async Task<QuestionResponseDto?> GetByIdAsync(Guid id)
    {
        var q = await _repository.GetByIdAsync(id);
        if (q == null) return null;
        return new QuestionResponseDto
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            CorrectAnswer = q.CorrectAnswer,
            Difficulty = q.Difficulty
        };
    }
}
