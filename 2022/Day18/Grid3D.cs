namespace Utils
{
    public class Grid3D : InfiniteGrid<Vector3Int, int>
    {
        public Grid3D()
        {

        }

        public int CountFacesNeighboringWith(int v)
        {
            return _grid
                .Where(pair => pair.Value == 1)
                .Select(pair => CountNeighborsWithValue(pair.Key, v))
                .Sum();
        }

        public IEnumerable<Vector3Int> NeighborsDelta()
        {
            yield return Vector3Int.Up;
            yield return Vector3Int.Down;
            yield return Vector3Int.Left;
            yield return Vector3Int.Right;
            yield return Vector3Int.Forward;
            yield return Vector3Int.Back;
        }

        public void FloodFill(Vector3Int p, int v)
        {
            if (GetValue(p) != 0)
                return;

            Stack<Vector3Int> PositionsToVisit = new();
            PositionsToVisit.Push(p);

            while (PositionsToVisit.Count > 0)
            {
                var newPos = PositionsToVisit.Pop();
                SetValue(newPos, v);
                foreach (var neighbor in NeighborsInBounds(newPos))
                    if (GetValue(neighbor) == 0)
                        PositionsToVisit.Push(neighbor);
            }
        }

        public IEnumerable<Vector3Int> NeighborsInBounds(Vector3Int p)
        {
            foreach (var n in NeighborsDelta())
            {
                var newP = p + n;
                if (IsInBounds(newP))
                    yield return newP;
            }
        }

        public bool IsInBounds(Vector3Int p)
        {
            return p.x >= BoundsMin.x && p.x <= BoundsMax.x
                && p.y >= BoundsMin.y && p.y <= BoundsMax.y
                && p.z >= BoundsMin.z && p.z <= BoundsMax.z;
        }

        public int CountNeighborsWithValue(Vector3Int p, int value)
        {
            return NeighborsDelta()
                .Select(n => n + p)
                .Where(n => GetValue(n) == value)
                .Count();
        }

        public bool AnyNeighborWithValue(Vector3Int p, int value)
        {
            return NeighborsDelta()
                .Select(n => n + p)
                .Any(n => GetValue(n) == value);
        }

        public void CalculateBounds()
        {
            BoundsMin = new(int.MaxValue, int.MaxValue, int.MaxValue);
            BoundsMax = new(int.MinValue, int.MinValue, int.MinValue);

            foreach (var pair in _grid)
            {
                BoundsMin = Vector3Int.Min(BoundsMin, pair.Key);
                BoundsMax = Vector3Int.Max(BoundsMax, pair.Key);
            }
        }

        public void ExpandBounds(int i)
        {
            CalculateBounds();
            BoundsMin -= Vector3Int.One * i;
            BoundsMax += Vector3Int.One * i;
        }


        public Vector3Int BoundsMin { get; protected set; }
        public Vector3Int BoundsMax { get; protected set; }
    }
}