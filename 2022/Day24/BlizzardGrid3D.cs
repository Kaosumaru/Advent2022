using Utils;

namespace Day24
{
    internal class BlizzardNode : GraphNode
    {
        public BlizzardNode(BlizzardGrid3D p, Vector3Int pos) { Parent = p; Position = pos; }

        public int Value;
        public Vector3Int Position { get; protected set; }
        public BlizzardGrid3D Parent { get; protected set; }

        public IEnumerable<GraphNode.ConnectionInfo> GetConnections()
        {
            return Parent.GetConnectionsFor(new Vector2Int(Position.x, Position.y), Position.z);
        }
    }

    internal class BlizzardPos
    {
        public enum Direction
        {
            East,
            West,
            North,
            South,
        }

        public BlizzardPos(int w, int h)
        {
            Width = w;
            Height = h;
            _blizzards = new int[w, h, 4];
        }

        public void Add(Vector2Int p, Direction dir)
        {
            _blizzards[p.x, p.y, (int)dir] = 1;
        }

        public int GetBlizzardsAt(Vector2Int p, int turn)
        {
            // east
            int sum = 0;
            int d = 0;
            foreach (var dir in directions)
            {
                var dp = p - dir * turn;
                dp.x = MathUtils.WrapAround(dp.x, Width);
                dp.y = MathUtils.WrapAround(dp.y, Height);

                sum += _blizzards[dp.x, dp.y, d];

                d++;
            }

            return sum;
        }

        protected static Vector2Int[] directions = new Vector2Int[] {
            new Vector2Int{ x = 1, y = 0},
            new Vector2Int{ x = -1, y = 0},
            new Vector2Int{ x = 0, y = -1},
            new Vector2Int{ x = 0, y = 1},
        };

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        int[,,] _blizzards;
    }


    internal class BlizzardGrid3D
    {
        public BlizzardGrid3D(int w, int h, int d)
        {
            Width = w;
            Height = h;

            Depth = d;

            _nodes = new BlizzardNode[Width, Height, Depth];

            for (int z = 0; z < Depth; z++)
                for (int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                        _nodes[x, y, z] = new BlizzardNode(this, new(x, y, z));
        }

        static public BlizzardGrid3D CreateGridFromFile(string path)
        {
            var lines = File.ReadAllLines(path);


            int w = lines[0].Length;
            int h = lines.Length;
            var grid = new GenericGrid<int>(w, h);
            BlizzardPos blizzardPos = new(w - 2, h - 2);
            Vector2Int offset = Vector2Int.One;
            foreach (var p in grid.AllPositions())
            {
                var c = lines[p.y][p.x];
                var wp = p - offset;

                if (c == '#')
                    grid.SetValue(p, 1);
                else if (c == '>')
                    blizzardPos.Add(wp, BlizzardPos.Direction.East);
                else if (c == '<')
                    blizzardPos.Add(wp, BlizzardPos.Direction.West);
                else if (c == '^')
                    blizzardPos.Add(wp, BlizzardPos.Direction.North);
                else if (c == 'v')
                    blizzardPos.Add(wp, BlizzardPos.Direction.South);
            }

            int d = MathUtils.Lcm(blizzardPos.Width, blizzardPos.Height);
            BlizzardGrid3D result = new(w, h, d);
            result.CopyGrid(grid);
            result.CopyBlizzard(blizzardPos, offset);
            return result;
        }

        internal void CopyGrid(GenericGrid<int> grid)
        {
            for (int t = 0; t < Depth; t++)
            {
                foreach (var p in grid.AllPositions())
                    SetValue(p, t, grid.GetValue(p));
            }
        }

        internal void CopyBlizzard(BlizzardPos blizzard, Vector2Int offset)
        {
            for (int t = 0; t < Depth; t++)
                for (int y = 0; y < blizzard.Height; y++)
                    for (int x = 0; x < blizzard.Width; x++)
                    {
                        var p = new Vector2Int(x, y);
                        int v = blizzard.GetBlizzardsAt(p, t);
                        SetValue(p + offset, t, v);
                    }
        }

        internal void Display()
        {
            for (int t = 0; t < Depth; t++)
            {
                Console.WriteLine($"Turn {t + 1}");
                DisplayTurn(t);
                Console.WriteLine();
            }
        }

        internal void DisplayTurn(int turn)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(GetValue(new Vector2Int(x, y), turn));
                Console.WriteLine();
            }
        }

        int GetValue(Vector2Int p, int turn)
        {
            return _nodes[p.x, p.y, turn].Value;
        }

        public BlizzardNode GetNode(Vector2Int p, int turn)
        {
            if (IsOutside(p))
                return null;
            return _nodes[p.x, p.y, turn];
        }

        void SetValue(Vector2Int p, int turn, int v)
        {
            _nodes[p.x, p.y, turn].Value = v;
        }

        public bool IsOutside(Vector2Int p)
        {
            return p.x < 0 || p.y < 0 || p.x >= Width || p.y >= Height;
        }

        public virtual IEnumerable<GraphNode.ConnectionInfo> GetConnectionsFor(Vector2Int p, int t)
        {
            t = (t + 1) % Depth;

            return Vector2IntExtensions.Neighbors4AndSelf(p)
                .Where(v => !IsOutside(v))
                .Where(v => GetValue(p, t) == 0)
                .Select(n => new GraphNode.ConnectionInfo { Distance = 1, Node = GetNode(n, t) });
        }

        public int PathFromTo(Vector2Int start, Vector2Int end, int turn)
        {
            turn = turn % Depth;

            DjikstraPath djikstra = new();
            djikstra.FindPathTo(GetNode(start, turn));

            return Enumerable.Range(0, Depth)
                .Select(z => GetNode(end, z))
                .Select(node => (int)djikstra.GetDistance(node))
                .Min() + 1;
        }

        public int FindShortestPathToEnd(int turn = 0)
        {
            var start = new Vector2Int(1, 0);
            var end = new Vector2Int(Width - 2, Height - 1);
            return PathFromTo(start, end, turn);
        }

        public int FindShortestPathToStart(int turn = 0)
        {
            var start = new Vector2Int(1, 0);
            var end = new Vector2Int(Width - 2, Height - 1);
            return PathFromTo(end, start, turn);
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int Depth { get; protected set; }

        BlizzardPos _blizzardPos;
        readonly BlizzardNode[,,] _nodes;
    }
}
