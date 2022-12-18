using Utils;

namespace Day17
{
    public class Well : IGrid<Vector2Long, bool>
    {
        public Well(long width)
        {
            _width = width;
        }

        public bool GetValue(Vector2Long p)
        {
            if (p.x < 0 || p.x >= _width)
                return true;
            if (p.y <= 0)
                return true;

            if (_grid.TryGetValue(p.y, out int v))
                return (v & (1 << (int)p.x)) != 0;

            return false;
        }

        public void SetValue(Vector2Long p, bool v)
        {
            _grid[p.y] = IntAt(p.y) | (1 << (int)p.x);
        }

        public int IntAt(long y)
        {
            _grid.TryGetValue(y, out int existing);
            return existing;
        }

        protected Dictionary<long, int> _grid = new();

        long _width = 0;
    }
}

