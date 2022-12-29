// See https://aka.ms/new-console-template for more information
using Day13;
using Utils;


List<List<Vector2Int>> GetPaths(string path)
{
    return File.ReadLines(path)
        .Select(InfiniteGridUtils.PathFromString)
        .ToList();
}

void Exercise1(string path)
{
    Vector2Int start = new(500, 0);

    var paths = GetPaths(path);
    var (min, max) = InfiniteGridUtils.MinMax(paths.SelectMany(x => x).Append(start));

    InfiniteGridThrowing grid = new(max.y + 1);
    InfiniteGridUtils.ApplyPathsToGrid(grid, paths, Vector2Int.Zero);

    int count = 0;

    try
    {
        while (true)
        {
            InfiniteGridUtils.DropSand(grid, start);
            count++;
        }
    }
    catch (ArgumentOutOfRangeException _)
    {

    }

    Console.WriteLine(count);
}

void Exercise2(string path)
{
    Vector2Int start = new(500, 0);

    var paths = GetPaths(path);
    var (min, max) = InfiniteGridUtils.MinMax(paths.SelectMany(x => x).Append(start));

    InfiniteGridGround grid = new(max.y + 2);
    InfiniteGridUtils.ApplyPathsToGrid(grid, paths, Vector2Int.Zero);

    int count = 0;

    while (true)
    {
        InfiniteGridUtils.DropSand(grid, start);
        count++;
        if (grid.GetValue(start) != 0)
            break;
    }

    Console.WriteLine(count);
}

string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);
