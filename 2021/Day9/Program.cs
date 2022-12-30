using Utils;

HeightMap CreateGridFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    var grid = new HeightMap(lines[0].Length, lines.Length);

    foreach (var p in grid.AllPositions())
    {
        int value = lines[p.y][p.x] - '0';
        grid.SetValue(p, value);
    }

    return grid;
}


string path = "../../../data2.txt";
var grid = CreateGridFromFile(path);
var sum = grid.Lowpoints()
    .Select(p => grid.GetValue(p) + 1)
    .Sum();
Console.WriteLine(sum);

var sum2 = grid.Lowpoints()
    .Select(p => grid.FloodFill(p))
    .OrderByDescending(p => p)
    .Take(3)
    .MultiplyTogether();

Console.WriteLine(sum2);