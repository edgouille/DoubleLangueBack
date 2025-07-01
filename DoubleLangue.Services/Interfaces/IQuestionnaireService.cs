using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IQuestionnaireService
{
    Task<QuestionnaireResponseDto> CreateAsync(QuestionnaireCreateDto dto);
    Task<QuestionnaireResponseDto?> GetByIdAsync(int id);
    Task AddQuestionAsync(int questionnaireId, int questionId, int order);
}
