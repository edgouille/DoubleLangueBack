namespace DoubleLangue.Domain.Dto.Question;

public class QuestionResponseDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public int Difficulty { get; set; } 
}
