namespace DoubleLangue.Domain.Dto.Question;

public class QuestionResponseDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
}
