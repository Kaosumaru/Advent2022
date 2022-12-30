using Utils;

RiskMap CreateGridFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    var grid = new RiskMap(lines[0].Length, lines.Length);

    foreach (var p in grid.AllPositions())
    {
        int value = lines[p.y][p.x] - '0';
        grid.SetValue(p, value);
    }

    return grid;
}

void Exercise1(string path)
{
    var grid = CreateGridFromFile(path);

    DjikstraPath djikstra = new();
    var start = grid.GetNode(Vector2Int.Zero);
    var end = grid.GetNode(new(grid.Width - 1, grid.Height - 1));
    djikstra.FindPathTo(start);

    Console.WriteLine(djikstra.GetDistance(end));
}

string path = "../../../data2.txt";
Exercise1(path);