using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Models;

public class Question
{
    [Key]
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public int Difficulty { get; set; } 

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public ICollection<QuestionnaireQuestion> QuestionnaireQuestions { get; set; } = new List<QuestionnaireQuestion>();
}
