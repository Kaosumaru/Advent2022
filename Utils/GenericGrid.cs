namespace Utils
{
    public class GridNode : DjikstraNode
    {
        public GridNode(GenericGrid p, Vector2Int pos) { Parent = p; Position = pos; }

        public int Value;
        public Vector2Int Position { get; protected set; }
        public GenericGrid Parent { get; protected set; }

        public IEnumerable<DjikstraNode.ConnectionInfo> GetConnections()
        {
            return Parent.GetConnectionsFor(Position);
        }
    }

    public interface IGrid<Key, Value>
    {
        public Value GetValue(Key p);
        public void SetValue(Key p, Value v);
    }

    public class GenericGrid : IGrid<Vector2Int, int>
    {
        public GenericGrid(int w, int h)
        {
            Width = w;
            Height = h;

            _nodes = new GridNode[w, h];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _nodes[x, y] = new GridNode(this, new(x, y));
        }

        public bool IsOutside(Vector2Int p)
        {
            return p.x < 0 || p.y < 0 || p.x >= Width || p.y >= Height;
        }

        public int GetValue(Vector2Int p)
        {
            if (IsOutside(p))
                return -1;
            return _nodes[p.x, p.y].Value;
        }

        public GridNode? GetNode(Vector2Int p)
        {
            if (IsOutside(p))
                return null;
            return _nodes[p.x, p.y];
        }

        public void SetValue(Vector2Int p, int v)
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

        public void Display(Func<int, string> transform)
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

        public virtual IEnumerable<DjikstraNode.ConnectionInfo> GetConnectionsFor(Vector2Int p)
        {
            return Neighbors4Of(p).Select(n => new DjikstraNode.ConnectionInfo { Distance = 1, Node = GetNode(n) });
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        readonly GridNode[,] _nodes;

        protected static Vector2Int[] directions = new Vector2Int[] {
            new Vector2Int{ x = 1, y = 0},
            new Vector2Int{ x = -1, y = 0},
            new Vector2Int{ x = 0, y = 1},
            new Vector2Int{ x = 0, y = -1},
        };
    }
}