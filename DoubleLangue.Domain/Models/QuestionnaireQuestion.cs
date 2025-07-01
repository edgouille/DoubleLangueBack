using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Models;

public class QuestionnaireQuestion
{
    [Key]
    public Guid Id { get; set; }
    public Guid QuestionnaireId { get; set; }
    public Guid QuestionId { get; set; }
    public int OrderInQuiz { get; set; }

    public Questionnaire? Questionnaire { get; set; }
    public Question? Question { get; set; }
}
