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

RiskMap DuplicateMap(RiskMap other, int size)
{
    var grid = new RiskMap(other.Width * size, other.Height * size);
    for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            var delta = new Vector2Int(x * other.Width, y * other.Height);
            foreach (var pos in other.AllPositions())
            {
                int v = other.GetValue(pos) - 1;
                v = MathUtils.WrapAround(v + x + y, 9) + 1;

                grid.SetValue(delta + pos, v);
            }
        }
    return grid;
}

void Exercise1(string path)
{
    var grid = CreateGridFromFile(path);
    var distance = grid.GetDistance(grid.BottomRight, grid.TopLeft);
    Console.WriteLine(distance);
}

void Exercise2(string path)
{
    var grid = DuplicateMap(CreateGridFromFile(path), 5);
    var distance = grid.GetDistance(grid.BottomRight, grid.TopLeft);
    Console.WriteLine(distance);
}

string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);