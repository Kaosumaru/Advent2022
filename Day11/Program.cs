// See https://aka.ms/new-console-template for more information
using Day11;
using System.Numerics;

void Exercise(string path, bool divide, int turns)
{
    MonkeysHolder h = new();
    h.ShouldDivide = divide;
    h.ParseMonkeys(path);

    for (int i = 0; i < turns; i++)
        h.Turn();

    var level = h.Monkeys
        .Select(m => m.InspectCount)
        .OrderByDescending(m => m)
        .Take(2)
        .Aggregate((BigInteger)1, (total, next) => total * next);

    Console.WriteLine(level);
}

string path = "../../../data2.txt";
Exercise(path, true, 20);
Exercise(path, false, 10000);

