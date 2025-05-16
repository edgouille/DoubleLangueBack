// See https://aka.ms/new-console-template for more information

using DoubleLangue.Services;
using DoubleLangue.Services.Interfaces;


var myService = new MathProblemGeneratorService();

var test = myService.GenerateMathProblem(3);


Console.WriteLine(test.Question);
Console.WriteLine(test.Answer);