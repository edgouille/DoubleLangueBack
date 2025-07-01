using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoubleLangue.Infrastructure.Repositories;

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly AppDbContext _context;
    public QuestionnaireRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire> AddAsync(Questionnaire questionnaire)
    {
        _context.Questionnaires.Add(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire;
    }

    public async Task AddQuestionAsync(QuestionnaireQuestion question)
    {
        _context.QuestionnaireQuestions.Add(question);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Questionnaire>> GetAllAsync()
    {
        return await _context.Questionnaires.ToListAsync();
    }

    public async Task<Questionnaire?> GetByIdAsync(int id)
    {
        return await _context.Questionnaires
            .Include(q => q.Questions)
            .ThenInclude(qq => qq.Question)
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}
