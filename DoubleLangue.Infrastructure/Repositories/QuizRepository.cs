using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Infrastructure.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Quiz> AddAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<Quiz?> GetByIdAsync(Guid id)
    {
        return await _context.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<QuizQuestion?> GetQuestionByIdAsync(Guid questionId)
    {
        return await _context.QuizQuestions.FindAsync(questionId);
    }

    public async Task UpdateQuestionAsync(QuizQuestion question)
    {
        _context.QuizQuestions.Update(question);
        await _context.SaveChangesAsync();
    }
}
