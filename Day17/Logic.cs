using Utils;

namespace Day17
{
    public class Logic
    {
        public Logic(List<Vector2Long> movement)
        {
            _shapes = Shape.Shapes();
            _movement = movement;
        }

        public void CalculateTurns(long t)
        {
            for (long i = 0; i < t; i++)
                CalculateTurn();
        }

        void CalculateTurn()
        {
            var shape = NextShape();
            var position = PositionForShape(shape);

            while (true)
            {
                var jetDirection = NextDirection();

                if (TryToFitShape(shape, position + jetDirection))
                    position += jetDirection;

                if (TryToFitShape(shape, position + Vector2Long.Down))
                    position += Vector2Long.Down;
                else
                    break;
            }

            AddShapeToWell(shape, position);

            Height = Math.Max(Height, position.y);
        }

        private void AddShapeToWell(Shape shape, Vector2Long position)
        {
            foreach (var point in shape.PointsWithOffset(position))
            {
                _well.SetValue(point, 1);
            }
        }

        private bool TryToFitShape(Shape shape, Vector2Long position)
        {
            foreach (var point in shape.PointsWithOffset(position))
            {
                if (_well.GetValue(point) != 0)
                    return false;
            }

            return true;
        }

        Vector2Long PositionForShape(Shape s)
        {
            long x = 2;
            long y = Height + 3 + s.Height;
            return new(x, y);
        }

        Shape NextShape()
        {
            var result = _shapes[_currentShape];
            _currentShape++;
            _currentShape %= _shapes.Count;
            return result;
        }

        private Vector2Long NextDirection()
        {
            var result = _movement[_currentMovement];
            _currentMovement++;
            _currentMovement %= _movement.Count;
            return result;
        }

        public void DebugDraw()
        {
            for (long y = Height + 2; y > 0; y--)
            {
                Console.Write("|");
                for (long x = 0; x < Width; x++)
                {
                    var v = _well.GetValue(new(x, y)) == 1;
                    Console.Write(v ? "#" : ".");
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("+-------+");
        }

        public const long Width = 7;
        public long Height { get; protected set; } = 0;

        List<Shape> _shapes;
        int _currentShape = 0;

        List<Vector2Long> _movement;
        int _currentMovement = 0;
        Well _well = new(Width);
    }


}

