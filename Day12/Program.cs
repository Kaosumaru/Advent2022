// See https://aka.ms/new-console-template for more information
using Day12;

MountainGrid CreateGridFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    var grid = new MountainGrid(lines[0].Length, lines.Length);

    foreach (var p in grid.AllPositions())
    {
        var c = lines[p.y][p.x];

        if (c == 'S')
            grid.SetStart(p);
        else if (c == 'E')
            grid.SetEnd(p);
        else
        {
            int value = c - 'a';
            grid.SetValue(p, value);
        }
    }


    return grid;
}

string path = "../../../data2.txt";
var grid = CreateGridFromFile(path);
Console.WriteLine(grid.FindShortestPathToStart());
Console.WriteLine(grid.FindShortestPossiblePath());