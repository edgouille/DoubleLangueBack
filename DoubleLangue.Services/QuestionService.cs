using DoubleLangue.Domain.Dto.Question;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _repository;

    public QuestionService(IQuestionRepository repository)
    {
        _repository = repository;
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
            Id = question.Id,
            QuestionText = question.QuestionText,
            CorrectAnswer = question.CorrectAnswer,
            Difficulty = question.Difficulty
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

    public async Task<QuestionResponseDto?> GetByIdAsync(int id)
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
