using Utils;

namespace Day22
{
    internal class TraversalCube
    {
        public TraversalCube(GenericGrid<int> grid, int size)
        {
            _grid = grid;
            _size = size;
            _info = new(_size, _grid.Width / _size, _grid);
        }

        public void Traverse(Orders orders)
        {
            Position = FindStartingPoint();
            Direction = 0;

            foreach (var order in orders.OrderList)
                ApplyOrder(order);
        }

        Vector2Int FindStartingPoint()
        {
            foreach (var p in _grid.AllPositions())
                if (_grid.GetValue(p) == 0)
                    return p;
            throw new Exception();
        }

        void ApplyOrder(Orders.Order order)
        {
            ApplyRotation(order.Rotate);
            ApplyForwardMovement(order.Forward);
        }

        void ApplyRotation(int r)
        {
            Direction += r;
            Direction = MathUtils.WrapAround(Direction, _direction.Length);
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
            var newDirection = Direction;

            int oldFace = _info.FaceOf(Position);
            int newFace = _info.FaceOf(newPos);

            if (newFace != oldFace)
            {
                (newPos, newDirection) = _info.PositionOnFace(Position, oldFace, Direction);
            }

            if (_grid.GetValue(newPos) != 0)
                return false;

            Position = newPos;
            Direction = newDirection;

            return true;
        }


        int _size;
        GenericGrid<int> _grid;
        public Vector2Int Position { get; protected set; }
        public int Direction { get; protected set; }

        FaceInfo _info;

        static Vector2Int[] _direction = new Vector2Int[]
        {
                    Vector2Int.Right,
                    Vector2Int.Up,
                    Vector2Int.Left,
                    Vector2Int.Down,
        };

    }
}
