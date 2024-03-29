﻿
using Utils;

class TreeGrid : GenericIntGrid
{
    public TreeGrid(int w, int h) : base(w, h)
    {

    }


    public bool CanTreeBeSeenFromAnyDirection(Vector2Int p)
    {
        return Vector2IntExtensions.Directions
            .Any(direction => CanTreeBeSeenFromDirection(p, direction));
    }

    bool CanTreeBeSeenFromDirection(Vector2Int start, Vector2Int direction)
    {
        var treeHeight = GetValue(start);
        return TreeHeightsInDirectionFrom(start, direction)
            .All(height => height < treeHeight);
    }

    public int TotalScenicScore(Vector2Int p)
    {
        return Vector2IntExtensions.Directions
            .Select(d => ScenicScoreForDirection(p, d))
            .MultiplyTogether();
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