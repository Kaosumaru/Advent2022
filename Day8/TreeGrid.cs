
using Utils;

class TreeGrid : GenericGrid
{
    public TreeGrid(int w, int h) : base(w, h)
    {

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
        var treeHeights = TreeHeightsInDirectionFrom(start, direction);

        int score = 0;
        foreach (var height in treeHeights)
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
        return TreeHeightsInDirectionFrom(start, direction)
            .All(height => height < treeHeight);
    }

    IEnumerable<int> TreeHeightsInDirectionFrom(Vector2Int start, Vector2Int direction)
    {
        return TreesInDirectionFrom(start, direction)
            .Select(GetValue);
    }

    IEnumerable<Vector2Int> TreesInDirectionFrom(Vector2Int start, Vector2Int direction)
    {
        while (true)
        {
            start += direction;
            if (IsOutside(start))
                yield break;
            yield return start;
        }
    }
}