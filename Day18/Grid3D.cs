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
            return p.x >= _boundsMin.x && p.x <= _boundsMax.x
                && p.y >= _boundsMin.y && p.y <= _boundsMax.y
                && p.z >= _boundsMin.z && p.z <= _boundsMax.z;
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
            _boundsMin = new(int.MaxValue, int.MaxValue, int.MaxValue);
            _boundsMax = new(int.MinValue, int.MinValue, int.MinValue);

            foreach (var pair in _grid)
            {
                _boundsMin = Vector3Int.Min(_boundsMin, pair.Key);
                _boundsMax = Vector3Int.Max(_boundsMax, pair.Key);
            }
        }

        public IEnumerable<Vector3Int> GetAllPositionsOnBounds()
        {
            //if (_boundsMin.x > _boundsMax.x || _boundsMin.y > _boundsMax.y || _boundsMin.z > _boundsMax.z)
            //    return ;

            // bottom
            return GetAllPositionsBetween(_boundsMin, new(_boundsMax.x, _boundsMin.y, _boundsMax.z))
                // top
                .Concat(GetAllPositionsBetween(new(_boundsMin.x, _boundsMax.y, _boundsMin.z), _boundsMax))
                // front
                .Concat(GetAllPositionsBetween(_boundsMin, new(_boundsMax.x, _boundsMax.x, _boundsMin.z)))
                // back
                .Concat(GetAllPositionsBetween(new(_boundsMin.x, _boundsMin.y, _boundsMax.z), _boundsMax))
                // left
                .Concat(GetAllPositionsBetween(_boundsMin, new(_boundsMin.x, _boundsMax.y, _boundsMax.z)))
                // right
                .Concat(GetAllPositionsBetween(new(_boundsMax.x, _boundsMin.y, _boundsMin.z), _boundsMax))
                ;


        }

        public IEnumerable<Vector3Int> GetAllPositionsBetween(Vector3Int a, Vector3Int b)
        {
            for (int x = a.x; x <= b.x; x++)
                for (int y = a.y; y <= b.y; y++)
                    for (int z = a.z; z <= b.z; z++)
                        yield return new Vector3Int(x, y, z);
        }

        public void ExpandBounds(int i)
        {
            CalculateBounds();
            _boundsMin -= Vector3Int.One * i;
            _boundsMax += Vector3Int.One * i;
        }

        Vector3Int _boundsMin;
        Vector3Int _boundsMax;
    }
}