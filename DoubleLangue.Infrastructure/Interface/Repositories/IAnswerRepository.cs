using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IAnswerRepository
{
    Task<Answer> AddAsync(Answer answer);
    Task<Answer?> GetByIdAsync(int id);
    Task<List<Answer>> GetByQuestionIdAsync(int questionId);
    Task<List<Answer>> GetByUserIdAsync(Guid userId);
}
