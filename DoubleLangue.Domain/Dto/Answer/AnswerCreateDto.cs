using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.Answer;

public class AnswerCreateDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid QuestionId { get; set; }
    [Required]
    public string UserAnswer { get; set; } = string.Empty;
}
