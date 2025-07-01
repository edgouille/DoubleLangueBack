using DoubleLangue.Domain.Dto.Answer;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;

    public AnswerService(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
    }

    public async Task<AnswerResponseDto> SaveAsync(AnswerCreateDto dto)
    {
        var question = await _questionRepository.GetByIdAsync(dto.QuestionId);
        if (question == null)
        {
            throw new Exception("Question not found");
        }
        var answer = new Answer
        {
            UserId = dto.UserId,
            QuestionId = dto.QuestionId,
            UserAnswer = dto.UserAnswer,
            IsCorrect = question.CorrectAnswer.Trim() == dto.UserAnswer.Trim(),
            AnsweredAt = DateTime.UtcNow
        };
        await _answerRepository.AddAsync(answer);
        return new AnswerResponseDto
        {
            Id = answer.Id,
            UserId = answer.UserId,
            QuestionId = answer.QuestionId,
            UserAnswer = answer.UserAnswer,
            IsCorrect = answer.IsCorrect,
            AnsweredAt = answer.AnsweredAt
        };
    }

    public async Task<AnswerResponseDto?> GetByIdAsync(int id)
    {
        var answer = await _answerRepository.GetByIdAsync(id);
        if (answer == null) return null;
        return new AnswerResponseDto
        {
            Id = answer.Id,
            UserId = answer.UserId,
            QuestionId = answer.QuestionId,
            UserAnswer = answer.UserAnswer,
            IsCorrect = answer.IsCorrect,
            AnsweredAt = answer.AnsweredAt
        };
    }
}
