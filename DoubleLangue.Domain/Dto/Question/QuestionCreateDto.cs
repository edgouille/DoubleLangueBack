using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.Question;

public class QuestionCreateDto
{
    [Required]
    public string QuestionText { get; set; } = string.Empty;
    [Required]
    public string CorrectAnswer { get; set; } = string.Empty;
    [Required]
    public string Difficulty { get; set; } = string.Empty;
}
