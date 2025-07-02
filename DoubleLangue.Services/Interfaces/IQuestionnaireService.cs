using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Domain.Models;
using DoubleLangue.Domain.Enum;

namespace DoubleLangue.Services.Interfaces;

public interface IQuestionnaireService
{
    Task<QuestionnaireResponseDto> CreateAsync(QuestionnaireCreateDto dto);
    Task<QuestionnaireResponseDto?> GetByIdAsync(Guid id);
    Task AddQuestionAsync(Guid questionnaireId, Guid questionId, int order);
    Task<List<QuestionnaireResponseDto>> GetAllAsync();
    Task<QuestionnaireResponseDto> GenerateAsync(int level, MathProblemType? type);
}
