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

            _turn++;

            AddShapeToWell(shape, position);

            Height = Math.Max(Height, position.y);

            _cachedHeights.Add(_turn, Height);

            if (Height == _prefixL)
            {
                if (_prefixT == -1 || _prefixT + 1 == _turn)
                    _prefixT = _turn;
            }
            else if ((Height - _prefixL) % _loopL == 0)
            {
                if (_loopT == -1 || _loopT + 1 == _turn)
                    _loopT = _turn;
            }
            else if (Height - _prefixL > _loopL && _loopT == -1)
            {
                _loopT = _turn;
                Console.WriteLine($"Edge case {_turn}, {Height - _prefixL}");
            }
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
#endif
        }

        public long CalculateAtTurn(long turn)
        {
            if (_prefixT == -1 || _loopT == -1)
            {
                Console.WriteLine($"Cached data is wrong");
                return -1;
            }

            long loopLength = _loopT - _prefixT;
            long prefixHeight = _cachedHeights[_prefixT];
            long loopHeight = _cachedHeights[_loopT] - _cachedHeights[_prefixT];

            long toDivide = turn - _prefixT;

            long times = toDivide / loopLength;
            long remainder = toDivide % loopLength;

            long result = times * loopHeight + _cachedHeights[_prefixT + remainder];


            return result;
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

        Dictionary<long, long> _cachedHeights = new();
        List<Vector2Long> _movement;
        int _currentMovement = 0;
        Well _well = new(Width);

        long _prefixL;
        long _loopL;
        long _prefixT = -1;
        long _loopT = -1;

        long _turn;
    }


}

