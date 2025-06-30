using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Services.Interfaces;

public interface IMathProblemGeneratorService
{
    MathProblem Generate(int level, MathProblemType type);
}
