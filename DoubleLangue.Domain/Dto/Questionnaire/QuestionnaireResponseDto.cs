namespace DoubleLangue.Domain.Dto.Questionnaire;

public class QuestionnaireResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<QuestionItemDto> Questions { get; set; } = [];
}

public class QuestionItemDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int OrderInQuiz { get; set; }
}
