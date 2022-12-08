struct Position
{
    public int x;
    public int y;
}

class TreeGrid
{
    public TreeGrid(int w, int h)
    {
        Width = w;
        Height = h;

        _treeHeights = new int[w, h];
    }

    public bool IsOutside(Position p)
    {
        return p.x < 0 || p.y < 0 || p.x >= Width || p.y >= Height;
    }

    public int GetValue(Position p)
    {
        if (IsOutside(p))
            return -1;
        return _treeHeights[p.x, p.y];
    }

    public void SetValue(Position p, int v)
    {
        if (IsOutside(p))
            return;
        _treeHeights[p.x, p.y] = v;
    }

    public IEnumerable<Position> AllPositions()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                yield return new Position { x = x, y = y };
    }

    public bool CanTreeBeSeenFromAnyDirection(Position p)
    {
        return directions.Any(direction => CanTreeBeSeenFromDirection(p, direction));
    }

    public int TotalScenicScore(Position p)
    {
        return directions.Select(d => ScenicScoreForDirection(p, d))
            .Aggregate(1, (total, next) => total * next);
    }

    int ScenicScoreForDirection(Position start, Position direction)
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

    bool CanTreeBeSeenFromDirection(Position start, Position direction)
    {
        var treeHeight = GetValue(start);
        return TreeHeightsInDirectionFrom(start, direction).All(height => height < treeHeight);
    }

    IEnumerable<int> TreeHeightsInDirectionFrom(Position start, Position direction)
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

    static Position[] directions = new Position[] {
            new Position{ x = 1, y = 0},
            new Position{ x = -1, y = 0},
            new Position{ x = 0, y = 1},
            new Position{ x = 0, y = -1},
        };
}