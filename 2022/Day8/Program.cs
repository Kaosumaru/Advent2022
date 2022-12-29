TreeGrid CreateGridFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    var grid = new TreeGrid(lines[0].Length, lines.Length);

    foreach (var p in grid.AllPositions())
    {
        int value = lines[p.y][p.x] - '0';
        grid.SetValue(p, value);
    }


    return grid;
}



string path = "../../../data2.txt";
var grid = CreateGridFromFile(path);
int visibleTrees = grid.AllPositions()
    .Where(p => grid.CanTreeBeSeenFromAnyDirection(p))
    .Count();

Console.WriteLine(visibleTrees);

int bestScore = grid.AllPositions()
    .Select(p => grid.TotalScenicScore(p))
    .Max();

Console.WriteLine(bestScore);
