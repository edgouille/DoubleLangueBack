using DoubleLangue.Domain.Dto.Answer;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IAnswerService
{
    Task<AnswerResponseDto> SaveAsync(AnswerCreateDto dto);
    Task<AnswerResponseDto?> GetByIdAsync(Guid id);
}
