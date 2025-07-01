using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IAnswerRepository
{
    Task<Answer> AddAsync(Answer answer);
    Task<Answer?> GetByIdAsync(Guid id);
    Task<List<Answer>> GetByQuestionIdAsync(Guid questionId);
    Task<List<Answer>> GetByUserIdAsync(Guid userId);
}
