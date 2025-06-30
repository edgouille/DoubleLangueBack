using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IQuizRepository
{
    Task<Quiz> AddAsync(Quiz quiz);
    Task<Quiz?> GetByIdAsync(Guid id);
    Task<QuizQuestion?> GetQuestionByIdAsync(Guid questionId);
    Task UpdateQuestionAsync(QuizQuestion question);
}
