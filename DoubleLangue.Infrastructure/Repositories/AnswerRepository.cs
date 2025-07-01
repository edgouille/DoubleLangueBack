using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Infrastructure.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly AppDbContext _context;
    public AnswerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Answer> AddAsync(Answer answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<Answer?> GetByIdAsync(Guid id)
    {
        return await _context.Answers.FindAsync(id);
    }

    public async Task<List<Answer>> GetByQuestionIdAsync(Guid questionId)
    {
        return await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
    }

    public async Task<List<Answer>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Answers.Where(a => a.UserId == userId).ToListAsync();
    }
}
