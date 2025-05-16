using System.Numerics;
using System.Text;
using DoubleLangue.Domain;
using DoubleLangue.Services.Interfaces;

namespace DoubleLangue.Services;

public class MathProblemGeneratorService : IMathProblemGeneratorService
{
    private static readonly char[] _operators = { '+', '-', '*', '/' };
    private readonly Random _random = new();

    public MathProblem GenerateMathProblem(int numberCount)
    {
        if (numberCount < 2) throw new ArgumentException("Minimum 2 nombres requis");

        var numbers = GenerateNumbers(numberCount);
        var chosenOperators = GenerateOperators(numberCount - 1);

        string question = BuildExpression(numbers, chosenOperators);
        double answer = CalculateAnswer(numbers, chosenOperators);

        return new MathProblem
        {
            Question = question,
            Answer = answer
        };
    }

    private int[] GenerateNumbers(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => _random.Next(-999, 1000))
            .ToArray();
    }

    private char[] GenerateOperators(int count)
    {
        return Enumerable.Range(0, count)
            .Select(_ => _operators[_random.Next(0, _operators.Length)])
            .ToArray();
    }

    private static string BuildExpression(int[] numbers, char[] operators)
    {
        var sb = new StringBuilder(FormatNumber(numbers[0]));
        for (int i = 1; i < numbers.Length; i++)
        {
            sb.Append($" {operators[i - 1]} ");
            sb.Append(FormatNumber(numbers[i]));
        }
        return sb.ToString();
    }

    private static string FormatNumber(int number)
    {
        return number < 0 ? $"({number})" : number.ToString();
    }

    private static double CalculateAnswer(int[] numbers, char[] operators)
    {
        // On va utiliser des listes pour pouvoir modifier la séquence
        var values = numbers.Select(n => (double)n).ToList();
        var ops = operators.ToList();

        // Étape 1 : traiter * et /
        for (int i = 0; i < ops.Count;)
        {
            if (ops[i] == '*' || ops[i] == '/')
            {
                double left = values[i];
                double right = values[i + 1];
                double result;

                if (ops[i] == '*')
                    result = left * right;
                else
                {
                    if (right == 0)
                        throw new DivideByZeroException("Division par zéro détectée dans l'expression.");
                    result = left / right;
                }

                // Remplacer les deux valeurs par le résultat et supprimer l'opérateur
                values[i] = result;
                values.RemoveAt(i + 1);
                ops.RemoveAt(i);
                // On ne fait pas i++ car la liste a changé
            }
            else
            {
                i++;
            }
        }

        // Étape 2 : traiter + et -
        double finalResult = values[0];
        for (int i = 0; i < ops.Count; i++)
        {
            if (ops[i] == '+')
                finalResult += values[i + 1];
            else // '-'
                finalResult -= values[i + 1];
        }

        return Math.Round(finalResult, 2);
    }
}
