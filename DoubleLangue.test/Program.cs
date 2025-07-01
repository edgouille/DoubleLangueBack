using DoubleLangue.Domain.Enum;

void test()
{
    var rdmtype = (MathProblemType)new Random().Next(0, 3);
    Console.Write(rdmtype);
}

test();