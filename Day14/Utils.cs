using Utils;

namespace Day13
{
    static class InfiniteGridUtils
    {

        public static Vector2Int VectorFromString(string str)
        {
            var s = str.Split(",");
            return new(int.Parse(s[0]), int.Parse(s[1]));
        }

        public static List<Vector2Int> PathFromString(string str)
        {
            return str.Split(" -> ").Select(VectorFromString).ToList();
        }

        public static (Vector2Int, Vector2Int) MinMax(IEnumerable<Vector2Int> list)
        {
            Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
            Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);
            foreach (var item in list)
            {
                if (item.x < min.x)
                    min.x = item.x;
                if (item.y < min.y)
                    min.y = item.y;
                if (item.x > max.x)
                    max.x = item.x;
                if (item.y > max.y)
                    max.y = item.y;
            }

            return (min, max);
        }

        public static void ApplyLineToGrid(IGrid grid, Tuple<Vector2Int, Vector2Int> line, Vector2Int offset)
        {
            var dir = (line.Item2 - line.Item1).Sign();
            var current = line.Item1;

            while (true)
            {
                grid.SetValue(current - offset, 1);
                if (current == line.Item2)
                    return;
                current += dir;
            }
        }

        public static void ApplyPathToGrid(IGrid grid, List<Vector2Int> path, Vector2Int offset)
        {
            var lines = path.Zip(path.Skip(1), Tuple.Create);
            foreach (var line in lines)
                ApplyLineToGrid(grid, line, offset);
        }

        public static void ApplyPathsToGrid(IGrid grid, IEnumerable<List<Vector2Int>> paths, Vector2Int offset)
        {
            foreach (var line in paths)
                ApplyPathToGrid(grid, line, offset);
        }

        public static bool IncrementPosition(IGrid grid, ref Vector2Int pos, Vector2Int delta)
        {
            var c = grid.GetValue(pos + delta);
            if (c == -1)
                throw new InvalidOperationException();
            if (c == 0)
            {
                pos += delta;
                return true;
            }
            return false;
        }

        public static void DropSand(IGrid grid, Vector2Int start)
        {
            Vector2Int pos = start;
            while (true)
            {
                if (IncrementPosition(grid, ref pos, Vector2Int.Up))
                    continue;
                if (IncrementPosition(grid, ref pos, Vector2Int.Up + Vector2Int.Left))
                    continue;
                if (IncrementPosition(grid, ref pos, Vector2Int.Up + Vector2Int.Right))
                    continue;
                break;
            }

            grid.SetValue(pos, 2);
        }
    }
}