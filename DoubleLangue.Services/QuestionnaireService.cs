using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class QuestionnaireService : IQuestionnaireService
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;

    public QuestionnaireService(IQuestionnaireRepository questionnaireRepository, IQuestionRepository questionRepository)
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
    }

    public async Task<QuestionnaireResponseDto> CreateAsync(QuestionnaireCreateDto dto)
    {
        var questionnaire = new Questionnaire
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };
        await _questionnaireRepository.AddAsync(questionnaire);
        return new QuestionnaireResponseDto
        {
            Id = questionnaire.Id,
            Title = questionnaire.Title,
            Description = questionnaire.Description,
            CreatedAt = questionnaire.CreatedAt
        };
    }

    public async Task AddQuestionAsync(int questionnaireId, int questionId, int order)
    {
        var question = await _questionRepository.GetByIdAsync(questionId);
        var questionnaire = await _questionnaireRepository.GetByIdAsync(questionnaireId);
        if (question == null || questionnaire == null)
            throw new Exception("Question or questionnaire not found");

        var qq = new QuestionnaireQuestion
        {
            QuestionnaireId = questionnaireId,
            QuestionId = questionId,
            OrderInQuiz = order
        };
        await _questionnaireRepository.AddQuestionAsync(qq);
    }

    public async Task<QuestionnaireResponseDto?> GetByIdAsync(int id)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(id);
        if (questionnaire == null) return null;
        return new QuestionnaireResponseDto
        {
            Id = questionnaire.Id,
            Title = questionnaire.Title,
            Description = questionnaire.Description,
            CreatedAt = questionnaire.CreatedAt,
            Questions = questionnaire.Questions.OrderBy(q => q.OrderInQuiz).Select(q => new QuestionItemDto
            {
                Id = q.QuestionId,
                QuestionText = q.Question?.QuestionText ?? string.Empty,
                OrderInQuiz = q.OrderInQuiz
            }).ToList()
        };
    }
}
