// See https://aka.ms/new-console-template for more information


using Day15;
using Utils;

Vector2Int CreateFromString(string str)
{
    var s = str.Split(", ");
    var x = int.Parse(s[0].Split("=")[1]);
    var y = int.Parse(s[1].Split("=")[1]);
    return new Vector2Int(x, y);
}

(Vector2Int, Vector2Int) VectorsFromString(string str)
{
    //Sensor at x=2, y=18: closest beacon is at x=-2, y=15

    str = str.Replace("Sensor at ", "");

    var middle = ": closest beacon is at ";
    var s = str.Split(middle);

    var pos = CreateFromString(s[0]);
    var beacon = CreateFromString(s[1]);

    return (pos, beacon);
}

Sensor SensorFromTuple((Vector2Int, Vector2Int) d)
{
    var w = Vector2Int.ManhattanDistance(d.Item1, d.Item2);
    return new Sensor(d.Item1, w);
}

IEnumerable<Vector2Int> GetPositionsAtY(Sensor s, int y)
{
    var start = new Vector2Int(s.Position.x, y);
    if (!s.Contains(start))
        yield break;

    yield return start;

    int delta = Math.Abs(s.Position.y - y);
    int w = s.Width - delta;

    for (int i = 0; i < w; i++)
    {
        yield return start + Vector2Int.Left * (i + 1);
        yield return start + Vector2Int.Right * (i + 1);
    }
}

void Exercise(string path, int y)
{
    var positions = File.ReadLines(path)
        .Select(VectorsFromString)
        .ToList();

    var filled = positions
        .Select(SensorFromTuple)
        .Select(s => GetPositionsAtY(s, y))
        .SelectMany(s => s)
        .ToHashSet();

    foreach (var pos in positions)
        filled.Remove(pos.Item2);

    Console.WriteLine(filled.Count);
}

string path = "../../../data.txt";
Exercise(path, 10);

checked
{
    // 5397366 too high
    string path2 = "../../../data2.txt";
    Exercise(path2, 2000000);
}



