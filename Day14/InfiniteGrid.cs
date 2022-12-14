using Utils;
namespace Day13
{
    public class InfiniteGrid : IGrid
    {
        virtual public int GetValue(Vector2Int p)
        {
            if (_grid.TryGetValue(p, out var v))
                return v;
            return 0;
        }

        public void SetValue(Vector2Int p, int v)
        {
            _grid[p] = v;
        }

        protected Dictionary<Vector2Int, int> _grid = new();
    }

    public class InfiniteGridThrowing : InfiniteGrid
    {
        public InfiniteGridThrowing(int max)
        {
            _max = max;
        }

        override public int GetValue(Vector2Int p)
        {
            if (p.y >= _max)
                throw new ArgumentOutOfRangeException();
            return base.GetValue(p);
        }

        int _max = 0;
    }

    public class InfiniteGridGround : InfiniteGrid
    {
        public InfiniteGridGround(int max)
        {
            _max = max;
        }

        override public int GetValue(Vector2Int p)
        {
            if (p.y >= _max)
                return 1;
            return base.GetValue(p);
        }

        int _max = 0;
    }
}

