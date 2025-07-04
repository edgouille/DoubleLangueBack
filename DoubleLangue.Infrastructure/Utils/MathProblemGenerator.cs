using DoubleLangue.Domain.Enum;
using DoubleLangue.Domain.Models;
using DoubleLangue.Infrastructure.Interface.Utils;

namespace DoubleLangue.Infrastructure.Utils;

public class MathProblemGenerator : IMathProblemGenerator
{
    private readonly Random _random = new();

    public MathProblem Generate(int level, MathProblemType type)
    {
        if (level < 0 || level > 2)
        {
            throw new ArgumentException("level must be between 0 and 2", nameof(level));
        }

        int maxValue = level switch
        {
            0 => 10,
            2 => 20,
            _ => 100
        };

        int a = _random.Next(1, maxValue + 1);
        int b = _random.Next(1, maxValue + 1);
        char op = "+-*/"[_random.Next(4)];
        double result = op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => Math.Round((double)a / b, 2),
            _ => 0
        };

        string question;
        string answer;

        switch (type)
        {
            case MathProblemType.MissingNumber:
                bool hideFirst = _random.Next(2) == 0;
                question = hideFirst ? $"X {op} {b} = {result}" : $"{a} {op} X = {result}";
                answer = hideFirst ? a.ToString() : b.ToString();
                break;
            case MathProblemType.MissingOperator:
                question = $"{a} X {b} = {result}";
                answer = op.ToString();
                break;
            default:
                question = $"{a} {op} {b} = X";
                answer = result.ToString();
                break;
        }

        return new MathProblem
        {
            Question = question,
            Answer = answer
        };
    }
}
