namespace Utils
{
    public class GenericGrid<T> : IGrid<Vector2Int, T>
    {
        public GenericGrid(int w, int h)
        {
            Width = w;
            Height = h;

            _nodes = new GridNode<T>[w, h];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _nodes[x, y] = new GridNode<T>(this, new(x, y));
        }

        public bool IsOutside(Vector2Int p)
        {
            return p.x < 0 || p.y < 0 || p.x >= Width || p.y >= Height;
        }

        protected virtual T GetDefaultValue()
        {
            return default(T);
        }

        public T GetValue(Vector2Int p)
        {
            if (IsOutside(p))
                return default(T);
            return _nodes[p.x, p.y].Value;
        }

        public GridNode<T>? GetNode(Vector2Int p)
        {
            if (IsOutside(p))
                return null;
            return _nodes[p.x, p.y];
        }

        public void SetValue(Vector2Int p, T v)
        {
            if (IsOutside(p))
                return;
            _nodes[p.x, p.y].Value = v;
        }

        public IEnumerable<Vector2Int> AllPositions()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return new Vector2Int { x = x, y = y };
        }

        public void Display(Func<T, string> transform)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(transform(GetValue(new Vector2Int(x, y))));
                Console.WriteLine();
            }
        }

        public IEnumerable<Vector2Int> Neighbors4Of(Vector2Int p)
        {
            foreach (var d in directions)
            {
                Vector2Int c = p + d;
                if (!IsOutside(c))
                    yield return c;
            }
        }

        public GenericGrid<T> SubGrid(Vector2Int start, Vector2Int size)
        {
            GenericGrid<T> newGrid = new(size.x, size.y);
            foreach (var p in newGrid.AllPositions())
            {
                newGrid.SetValue(p, GetValue(p + start));
            }
            return newGrid;
        }

        public virtual IEnumerable<DjikstraNode.ConnectionInfo> GetConnectionsFor(Vector2Int p)
        {
            return Neighbors4Of(p).Select(n => new DjikstraNode.ConnectionInfo { Distance = 1, Node = GetNode(n) });
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        readonly GridNode<T>[,] _nodes;

        protected static Vector2Int[] directions = new Vector2Int[] {
            new Vector2Int{ x = 1, y = 0},
            new Vector2Int{ x = -1, y = 0},
            new Vector2Int{ x = 0, y = 1},
            new Vector2Int{ x = 0, y = -1},
        };
    }

    public class GenericIntGrid : GenericGrid<int>
    {
        public GenericIntGrid(int w, int h) : base(w, h)
        {
        }

        protected override int GetDefaultValue()
        {
            return -1;
        }
    }

}