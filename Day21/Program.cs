using Day21;

Monkeys GetMonkeysFor(string path, bool equals)
{
    var monkeys = new Monkeys(equals);
    foreach (var line in File.ReadLines(path))
    {
        var s = line.Split(": ");
        monkeys.AddLine(s[0], s[1]);
    }
    return monkeys;
}

long Calculate(string path)
{
    return GetMonkeysFor(path, false).CalculateFor("root");
}

long Solve(string path)
{
    return GetMonkeysFor(path, true).SolveFor("root");
}

string path = "../../../data2.txt";
Console.WriteLine(Calculate(path));
Console.WriteLine(Solve(path));