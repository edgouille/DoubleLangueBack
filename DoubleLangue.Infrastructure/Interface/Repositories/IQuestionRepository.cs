using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IQuestionRepository
{
    Task<Question> AddAsync(Question question);
    Task<Question?> GetByIdAsync(Guid id);
    Task<List<Question>> GetAllAsync();
}
