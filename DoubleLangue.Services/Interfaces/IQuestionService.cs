using DoubleLangue.Domain.Dto.Question;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IQuestionService
{
    Task<QuestionResponseDto> CreateAsync(QuestionCreateDto dto);
    Task<QuestionResponseDto?> GetByIdAsync(int id);
    Task<List<QuestionResponseDto>> GetAllAsync();
}
