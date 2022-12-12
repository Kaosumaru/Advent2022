
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
}