using Utils;

namespace Day12
{
    internal class MountainGrid : GenericGrid
    {
        public MountainGrid(int w, int h) : base(w, h)
        {
        }

        internal void SetStart(Vector2Int p)
        {
            SetValue(p, 0);
            _start = p;
        }

        internal void SetEnd(Vector2Int p)
        {
            SetValue(p, 'z' - 'a');
            _end = p;
        }

        bool CanTraverseFrom(Vector2Int from, Vector2Int to)
        {
            var v1 = GetValue(from);
            var v2 = GetValue(to);
            return v1 - v2 <= 1;
        }

        public override IEnumerable<DjikstraNode.ConnectionInfo> GetConnectionsFor(Vector2Int p)
        {
            return Neighbors4Of(p)
                .Where(n => CanTraverseFrom(p, n))
                .Select(n => new DjikstraNode.ConnectionInfo { Distance = 1, Node = GetNode(n) });
        }

        public int FindShortestPathToStart()
        {
            DjikstraPath djikstra = new();
            djikstra.FindPathTo(GetNode(_end));
            return (int)djikstra.GetDistance(GetNode(_start));
        }

        public int FindShortestPossiblePath()
        {
            DjikstraPath djikstra = new();
            djikstra.FindPathTo(GetNode(_end));

            return (int)AllPositions()
                .Where(pos => GetValue(pos) == 0)
                .Min(pos => djikstra.GetDistance(GetNode(pos)));

        }

        Vector2Int _start;
        Vector2Int _end;
    }
}
