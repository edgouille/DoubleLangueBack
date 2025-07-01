using DoubleLangue.Domain.Dto.Question;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IQuestionService
{
    Task<QuestionResponseDto> CreateAsync(QuestionCreateDto dto);
    Task<QuestionResponseDto> GenerateAsync(int level, MathProblemType type);
    Task<QuestionResponseDto?> GetByIdAsync(Guid id);
    Task<List<QuestionResponseDto>> GetAllAsync();
}
