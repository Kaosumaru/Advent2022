// See https://aka.ms/new-console-template for more information
using Utils;

Vector3Int VectorFromString(string str)
{
    var s = str.Split(",");
    Vector3Int v = Vector3Int.Zero;
    for (int i = 0; i < 3; i++)
        v[i] = int.Parse(s[i]);
    return v;
}

string path = "../../../data2.txt";

var positions = File.ReadLines(path).Select(VectorFromString);
Grid3D grid = new();
foreach (var v in positions)
    grid.SetValue(v, 1);

Console.WriteLine(grid.CountFacesNeighboringWith(0));

grid.ExpandBounds(1);

foreach (var pos in grid.GetAllPositionsOnBounds())
{
    grid.FloodFill(pos, 2);
}

Console.WriteLine(grid.CountFacesNeighboringWith(2));
