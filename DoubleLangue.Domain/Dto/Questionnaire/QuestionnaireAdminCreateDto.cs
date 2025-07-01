using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.Questionnaire;

public class QuestionnaireAdminCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime ExamDateTime { get; set; }
    public int Level { get; set; } = 1;
    public List<Guid> QuestionsList { get; set; } = new();
}
