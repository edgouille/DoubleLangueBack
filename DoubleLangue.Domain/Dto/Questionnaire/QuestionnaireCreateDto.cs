using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.Questionnaire;

public class QuestionnaireCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
