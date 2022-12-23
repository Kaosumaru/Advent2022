using Utils;

namespace Day22
{
    internal class TraversalGrid
    {
        public TraversalGrid(GenericGrid<int> grid)
        {
            _grid = grid;
        }

        public void Traverse(Orders orders)
        {
            Position = FindStartingPoint();
            Direction = 0;

            foreach (var order in orders.OrderList)
                ApplyOrder(order);
        }

        void ApplyOrder(Orders.Order order)
        {
            ApplyRotation(order.Rotate);
            ApplyForwardMovement(order.Forward);
        }

        int wrapAround(int v, int maxval)
        {
            if (v < 0)
                return maxval + v;
            return v % maxval;
        }

        void ApplyRotation(int r)
        {
            Direction += r;
            Direction = wrapAround(Direction, _direction.Length);
        }

        void ApplyForwardMovement(int forward)
        {
            for (int i = 0; i < forward; i++)
                if (!ApplyForwardMovement())
                    return;
        }

        bool ApplyForwardMovement()
        {
            var delta = _direction[Direction];
            var newPos = Position + delta;

            while (true)
            {
                newPos.x = wrapAround(newPos.x, _grid.Width);
                newPos.y = wrapAround(newPos.y, _grid.Height);

                int v = _grid.GetValue(newPos);

                if (v == 2)
                {
                    newPos += delta;
                    continue;
                }

                if (v == 1)
                {
                    return false;
                }

                break;
            }

            Position = newPos;
            return true;
        }

        Vector2Int FindStartingPoint()
        {
            foreach (var p in _grid.AllPositions())
                if (_grid.GetValue(p) == 0)
                    return p;
            throw new Exception();
        }


        GenericGrid<int> _grid;
        public Vector2Int Position { get; protected set; }
        public int Direction { get; protected set; }

        static Vector2Int[] _direction = new Vector2Int[]
        {
            Vector2Int.Right,
            Vector2Int.Up,
            Vector2Int.Left,
            Vector2Int.Down,
        };
    }
}
