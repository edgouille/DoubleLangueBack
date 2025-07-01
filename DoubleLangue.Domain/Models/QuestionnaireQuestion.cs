using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Models;

public class QuestionnaireQuestion
{
    [Key]
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public int QuestionId { get; set; }
    public int OrderInQuiz { get; set; }

    public Questionnaire? Questionnaire { get; set; }
    public Question? Question { get; set; }
}
