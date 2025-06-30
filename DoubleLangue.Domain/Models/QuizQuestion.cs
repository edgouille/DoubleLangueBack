using System.ComponentModel.DataAnnotations;

namespace DoubleLangue.Domain.Models;

public class QuizQuestion
{
    [Key]
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string Question { get; set; }
    public string CorrectAnswer { get; set; }
    public string? UserAnswer { get; set; }

    public Quiz? Quiz { get; set; }
}
