using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DoubleLangue.Domain.Models;

public class Quiz
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<QuizQuestion> Questions { get; set; } = new();
}
