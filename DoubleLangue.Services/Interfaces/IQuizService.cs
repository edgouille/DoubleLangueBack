using DoubleLangue.Domain.Dto.Quiz;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IQuizService
{
    Task<QuizResponseDto> CreateAsync(Guid userId, int level, MathProblemType type);
    Task<Quiz?> GetByIdAsync(Guid id);
    Task<bool> SaveAnswerAsync(Guid questionId, string answer);
}
