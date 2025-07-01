using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Models;

public class Answer
{
    [Key]
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int QuestionId { get; set; }
    public string UserAnswer { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }

    public User? User { get; set; }
    public Question? Question { get; set; }
}
