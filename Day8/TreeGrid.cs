
using Utils;

class TreeGrid
{
    public TreeGrid(int w, int h)
    {
        Width = w;
        Height = h;

        _treeHeights = new int[w, h];
    }

    public bool IsOutside(Vector2Int p)
    {
        return p.x < 0 || p.y < 0 || p.x >= Width || p.y >= Height;
    }

    public int GetValue(Vector2Int p)
    {
        if (IsOutside(p))
            return -1;
        return _treeHeights[p.x, p.y];
    }

    public void SetValue(Vector2Int p, int v)
    {
        if (IsOutside(p))
            return;
        _treeHeights[p.x, p.y] = v;
    }

    public IEnumerable<Vector2Int> AllPositions()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                yield return new Vector2Int { x = x, y = y };
    }

    public bool CanTreeBeSeenFromAnyDirection(Vector2Int p)
    {
        return directions.Any(direction => CanTreeBeSeenFromDirection(p, direction));
    }

    public int TotalScenicScore(Vector2Int p)
    {
        return directions.Select(d => ScenicScoreForDirection(p, d))
            .Aggregate(1, (total, next) => total * next);
    }

    int ScenicScoreForDirection(Vector2Int start, Vector2Int direction)
    {
        var treeHeight = GetValue(start);
        var trees = TreeHeightsInDirectionFrom(start, direction);

        int score = 0;
        foreach (var height in trees)
        {
            score++;
            if (height >= treeHeight)
                break;
        }
        return score;
    }

    bool CanTreeBeSeenFromDirection(Vector2Int start, Vector2Int direction)
    {
        var treeHeight = GetValue(start);
        return TreeHeightsInDirectionFrom(start, direction).All(height => height < treeHeight);
    }

    IEnumerable<int> TreeHeightsInDirectionFrom(Vector2Int start, Vector2Int direction)
    {
        while (true)
        {
            start.x += direction.x;
            start.y += direction.y;
            if (IsOutside(start))
                yield break;
            yield return GetValue(start);
        }
    }

    public int Width { get; protected set; }
    public int Height { get; protected set; }

    readonly int[,] _treeHeights;

    static Vector2Int[] directions = new Vector2Int[] {
            new Vector2Int{ x = 1, y = 0},
            new Vector2Int{ x = -1, y = 0},
            new Vector2Int{ x = 0, y = 1},
            new Vector2Int{ x = 0, y = -1},
        };
}