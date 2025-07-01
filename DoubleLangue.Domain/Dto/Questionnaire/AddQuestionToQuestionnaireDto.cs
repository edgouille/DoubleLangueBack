using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Dto.Questionnaire;

public class AddQuestionToQuestionnaireDto
{
    [Required]
    public int QuestionId { get; set; }
    [Required]
    public int OrderInQuiz { get; set; }
}
