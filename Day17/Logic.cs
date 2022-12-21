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

        public void CalculateTurns(long t, long prefixL, long loopL)
        {
            _prefixL = prefixL;
            _loopL = loopL;

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

            var oldHeight = Height;
            Height = Math.Max(Height, position.y);

            _heightDelta.Add((int)(Height - oldHeight));

        }

        private void AddShapeToWell(Shape shape, Vector2Long position)
        {
            foreach (var point in shape.PointsWithOffset(position))
            {
                _well.SetValue(point, true);
            }
        }

        private bool TryToFitShape(Shape shape, Vector2Long position)
        {
            foreach (var point in shape.PointsWithOffset(position))
            {
                if (_well.GetValue(point))
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

        public void DebugInfo()
        {
#if false
            Console.WriteLine($"Prefix {_prefixT}, loop {_loopT - _prefixT}");
            for (long y = 1; y <= Height; y++)
            {
                Console.Write("{0:X2} ", _well.IntAt(y));
            }

            Console.WriteLine();
#else
            foreach (var h in _heightDelta)
                Console.Write(h);
            Console.WriteLine();
#endif
        }

        public long CalculateAtTurn(long turn)
        {
            long prefixHeight = _heightDelta.Take((int)_prefixL).Select(x => (long)x).Sum();
            long loopHeight = _heightDelta.Skip((int)_prefixL).Take((int)_loopL).Select(x => (long)x).Sum();


            long height = prefixHeight;
            turn -= _prefixL;

            long loops = turn / _loopL;
            long left = turn % _loopL;

            height += loops * loopHeight;

            height += _heightDelta.Skip((int)_prefixL).Take((int)left).Select(x => (long)x).Sum();

            return height;
        }

        public void DebugDraw()
        {
            for (long y = Height + 2; y > 0; y--)
            {
                Console.Write("|");
                for (long x = 0; x < Width; x++)
                {
                    var v = _well.GetValue(new(x, y));
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
        List<int> _heightDelta = new();
        int _currentMovement = 0;
        Well _well = new(Width);

        long _prefixL;
        long _loopL;
    }


}

