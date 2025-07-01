using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Repositories;

public interface IQuestionnaireRepository
{
    Task<Questionnaire> AddAsync(Questionnaire questionnaire);
    Task<Questionnaire?> GetByIdAsync(int id);
    Task<List<Questionnaire>> GetAllAsync();
    Task AddQuestionAsync(QuestionnaireQuestion question);
}
