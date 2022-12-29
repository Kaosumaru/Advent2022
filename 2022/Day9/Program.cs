using Day9;
using Utils;

IEnumerable<MoveCommand> GetCommands(string path)
{
    return File.ReadLines(path).Select(MoveCommand.FromString);
}

int Exercise(string path, int l)
{
    HashSet<Vector2Int> set = new();
    Line line = new(l);
    set.Add(line.Tail);
    line.OnHeadMoved = l => set.Add(l.Tail);

    foreach (var command in GetCommands(path))
        line.ApplyCommand(command);

    return set.Count;
}

string path = "../../../data2.txt";
Console.WriteLine(Exercise(path, 2));
Console.WriteLine(Exercise(path, 10));

