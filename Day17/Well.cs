using Utils;

namespace Day17
{
    public class Well : InfiniteGrid<Vector2Long, int>
    {
        public Well(long width)
        {
            _width = width;
        }

        override public int GetValue(Vector2Long p)
        {
            if (p.x < 0 || p.x >= _width)
                return 1;
            if (p.y <= 0)
                return 1;

            return base.GetValue(p);
        }

        long _width = 0;
    }
}

