using DoubleLangue.Domain.Dto.Questionnaire;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;
using System;

using System.Linq;

namespace DoubleLangue.Services;

public class QuestionnaireService : IQuestionnaireService
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuestionService _questionService;

    public QuestionnaireService(IQuestionnaireRepository questionnaireRepository, IQuestionRepository questionRepository, IQuestionService questionService)
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _questionService = questionService;
    }

    public async Task<QuestionnaireResponseDto> CreateAsync(QuestionnaireCreateDto dto)
    {
        var questionnaire = new Questionnaire
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            ExamDateTime = dto.ExamDateTime
        };
        await _questionnaireRepository.AddAsync(questionnaire);
        return new QuestionnaireResponseDto
        {
            Id = questionnaire.Id,
            Title = questionnaire.Title,
            Description = questionnaire.Description,
            CreatedAt = questionnaire.CreatedAt,
            ExamDateTime = questionnaire.ExamDateTime
        };
    }

    public async Task<QuestionnaireResponseDto> GenerateAsync(int level, MathProblemType? type, bool isTest)
    {
        var questionnaire = new Questionnaire
        {
            Title = $"Generated Questionnaire of {DateTime.UtcNow.Date}" ,
            Description = "Generated questionnaire",
            CreatedAt = DateTime.UtcNow,
            ExamDateTime = DateTime.UtcNow
        };
        questionnaire = await _questionnaireRepository.AddAsync(questionnaire);
        for (var i = 0; i < 10; i++)
        {
            var currentType = type ?? (MathProblemType)new Random().Next(0, 3);
            var question = await _questionService.GenerateAsync(level, currentType);
            await _questionnaireRepository.AddQuestionAsync(new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id,
                OrderInQuiz = i
            });
        }

        questionnaire = await _questionnaireRepository.GetByIdAsync(questionnaire.Id);

        var qq = questionnaire?.Questions.OrderBy(q => q.OrderInQuiz).Select(q => new QuestionItemDto
        {
            Id = q.QuestionId,
            QuestionText = q.Question?.QuestionText ?? string.Empty,
            CorrectAnswer = isTest ? null : q.Question?.CorrectAnswer,
            OrderInQuiz = q.OrderInQuiz
        }).ToList() ?? new List<QuestionItemDto>();

        return new QuestionnaireResponseDto
        {
            Id = questionnaire.Id,
            Title = questionnaire.Title,
            Description = questionnaire.Description,
            CreatedAt = DateTime.UtcNow,
            ExamDateTime = questionnaire.ExamDateTime,
            Questions = qq
        };

    }

    public async Task AddQuestionAsync(Guid questionnaireId, Guid questionId, int order)
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

    public async Task<QuestionnaireResponseDto?> GetByIdAsync(Guid id)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(id);
        if (questionnaire == null) return null;
        return new QuestionnaireResponseDto
        {
            Id = questionnaire.Id,
            Title = questionnaire.Title,
            Description = questionnaire.Description,
            CreatedAt = questionnaire.CreatedAt,
            ExamDateTime = questionnaire.ExamDateTime,
            Questions = questionnaire.Questions.OrderBy(q => q.OrderInQuiz).Select(q => new QuestionItemDto
            {
                Id = q.QuestionId,
                QuestionText = q.Question?.QuestionText ?? string.Empty,
                OrderInQuiz = q.OrderInQuiz
            }).ToList()
        };
    }

    public async Task<List<QuestionnaireResponseDto>> GetAllAsync()
    {
        var questionnaires = await _questionnaireRepository.GetAllAsync();
        return questionnaires.Select(q => new QuestionnaireResponseDto
        {
            Id = q.Id,
            Title = q.Title,
            Description = q.Description,
            CreatedAt = q.CreatedAt,
            ExamDateTime = q.ExamDateTime,
            Questions = q.Questions.OrderBy(q => q.OrderInQuiz).Select(q => new QuestionItemDto
            {
                Id = q.QuestionId,
                QuestionText = q.Question?.QuestionText ?? string.Empty,
                OrderInQuiz = q.OrderInQuiz
            }).ToList()
        }).ToList();
    }

    public async Task<int> CheckAnswersAsync(Guid questionnaireId, List<QuestionAnswerDto> answers)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(questionnaireId);
        if (questionnaire == null)
            throw new Exception("Questionnaire not found");

        int score = 0;
        foreach (var ans in answers)
        {
            var question = questionnaire.Questions.FirstOrDefault(q => q.QuestionId == ans.QuestionId)?.Question;
            if (question == null) continue;
            if (question.CorrectAnswer.Trim().Equals(ans.Answer.Trim(), StringComparison.OrdinalIgnoreCase))
                score++;
        }
        return score;
    }
}
