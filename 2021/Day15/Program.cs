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
    var distance = grid.GetDistance(grid.TopLeft, grid.BottomRight);
    Console.WriteLine(distance);
}

string path = "../../../data2.txt";
Exercise1(path);