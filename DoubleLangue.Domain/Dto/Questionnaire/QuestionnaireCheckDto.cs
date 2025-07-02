namespace DoubleLangue.Domain.Dto.Questionnaire;

public class QuestionnaireCheckDto
{
    public Guid QuestionnaireId { get; set; }
    public List<QuestionAnswerDto> Answers { get; set; } = new();
}

public class QuestionAnswerDto
{
    public Guid QuestionId { get; set; }
    public string Answer { get; set; } = string.Empty;
}
