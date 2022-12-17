using Utils;

namespace Day15
{
    internal class Sensor
    {
        public Sensor()
        {

        }

        public Sensor(Vector2Int pos, int width)
        {
            Position = pos;
            Width = width;
        }

        public bool Contains(Vector2Int pos)
        {
            int d = Vector2Int.ManhattanDistance(Position, pos);
            return d <= Width;
        }

        public IEnumerable<Vector2Int> PositionsInBorder()
        {
            if (Width == 0)
            {
                yield return Position;
                yield break;
            }

            // return center row
            yield return Position + Vector2Int.Left * Width;
            yield return Position + Vector2Int.Right * Width;

            // return top & bottom
            yield return Position + Vector2Int.Up * Width;
            yield return Position + Vector2Int.Down * Width;

            // return all other positions
            for (int i = 1; i < Width; i++)
            {
                var dx = Vector2Int.Left * (Width - i);
                var dy = Vector2Int.Up * i;

                yield return Position + dy + dx;
                yield return Position + dy - dx;

                yield return Position - dy + dx;
                yield return Position - dy - dx;
            }
        }

        public Vector2Int Position;
        public int Width;
    }
}
