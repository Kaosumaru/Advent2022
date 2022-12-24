namespace Utils
{
    public class InfiniteGrid<Key, Value> : IGrid<Key, Value>
    {
        virtual public Value GetValue(Key p)
        {
            if (_grid.TryGetValue(p, out var v))
                return v;
            return default(Value);
        }

        public void SetValue(Key p, Value v)
        {
            _grid[p] = v;
        }

        public void RemoveValue(Key p)
        {
            _grid.Remove(p);
        }

        public Dictionary<Key, Value> GetGrid()
        {
            return _grid;
        }

        protected Dictionary<Key, Value> _grid = new();
    }

    public class InfiniteGridThrowing : InfiniteGrid<Vector2Int, int>
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

    public class InfiniteGridGround : InfiniteGrid<Vector2Int, int>
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

