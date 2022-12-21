using Day21;

long Calculate(string path)
{
    var monkeys = new Monkeys(false);
    foreach (var line in File.ReadLines(path))
    {
        var s = line.Split(": ");
        monkeys.AddLine(s[0], s[1]);
    }

    return monkeys.CalculateFor("root");
}

long Solve(string path)
{
    var monkeys = new Monkeys(true);
    foreach (var line in File.ReadLines(path))
    {
        var s = line.Split(": ");
        monkeys.AddLine(s[0], s[1]);
    }

    return monkeys.SolveFor("root");
}

string path = "../../../data2.txt";
Console.WriteLine(Calculate(path));
Console.WriteLine(Solve(path));