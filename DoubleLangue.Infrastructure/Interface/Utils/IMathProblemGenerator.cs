using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;

namespace DoubleLangue.Infrastructure.Interface.Utils;

public interface IMathProblemGenerator
{
    MathProblem Generate(int level, MathProblemType type);
}
