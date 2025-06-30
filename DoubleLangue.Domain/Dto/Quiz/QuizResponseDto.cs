namespace DoubleLangue.Domain.Dto.Quiz;

public class QuizResponseDto
{
    public Guid Id { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
}
