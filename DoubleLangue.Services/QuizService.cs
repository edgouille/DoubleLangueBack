using DoubleLangue.Domain.Dto.Quiz;
using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly IMathProblemGeneratorService _generator;

    public QuizService(IQuizRepository quizRepository, IMathProblemGeneratorService generator)
    {
        _quizRepository = quizRepository;
        _generator = generator;
    }

    public async Task<QuizResponseDto> CreateAsync(Guid userId, int level, MathProblemType type)
    {
        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        for (int i = 0; i < 10; i++)
        {
            var problem = _generator.Generate(level, type);
            quiz.Questions.Add(new QuizQuestion
            {
                Id = Guid.NewGuid(),
                QuizId = quiz.Id,
                Question = problem.Question,
                CorrectAnswer = problem.Answer
            });
        }

        await _quizRepository.AddAsync(quiz);

        return new QuizResponseDto
        {
            Id = quiz.Id,
            Questions = quiz.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Question = q.Question
            }).ToList()
        };
    }

    public async Task<Quiz?> GetByIdAsync(Guid id)
    {
        return await _quizRepository.GetByIdAsync(id);
    }

    public async Task<bool> SaveAnswerAsync(Guid questionId, string answer)
    {
        var question = await _quizRepository.GetQuestionByIdAsync(questionId);
        if (question is null)
        {
            return false;
        }

        question.UserAnswer = answer;
        await _quizRepository.UpdateQuestionAsync(question);
        return true;
    }
}
