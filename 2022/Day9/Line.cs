using Utils;

namespace Day9
{
    struct MoveCommand
    {
        public Vector2Int direction;
        public int distance;

        public static MoveCommand FromString(string str)
        {
            MoveCommand cmd = new();
            if (str[0] == 'U')
                cmd.direction = Vector2Int.Up;
            else if (str[0] == 'D')
                cmd.direction = Vector2Int.Down;
            else if (str[0] == 'L')
                cmd.direction = Vector2Int.Left;
            else
                cmd.direction = Vector2Int.Right;

            cmd.distance = int.Parse(str.AsSpan(2));
            return cmd;
        }
    }

    internal class Line
    {
        public Line(int length)
        {
            Knots = new Vector2Int[length];
        }

        public void ApplyCommand(MoveCommand c)
        {
            for (int i = 0; i < c.distance; i++)
                MoveHead(c.direction);
        }

        public void MoveHead(Vector2Int d)
        {
            Head += d;

            for (int i = 1; i < Knots.Length; i++)
            {
                if (!ShouldKnotMove(i))
                    break;

                MoveKnot(i);
            }

            OnHeadMoved?.Invoke(this);
        }

        void MoveKnot(int i)
        {
            var dx = Math.Sign(Knots[i - 1].x - Knots[i].x);
            var dy = Math.Sign(Knots[i - 1].y - Knots[i].y);

            Knots[i] += new Vector2Int(dx, dy);
        }

        bool ShouldKnotMove(int i)
        {
            var dx = Math.Abs(Knots[i - 1].x - Knots[i].x);
            var dy = Math.Abs(Knots[i - 1].y - Knots[i].y);
            return dx > 1 || dy > 1;
        }

        public Action<Line>? OnHeadMoved;

        public Vector2Int Head { get { return Knots[0]; } set { Knots[0] = value; } }
        public Vector2Int Tail { get { return Knots.Last(); } }
        public Vector2Int[] Knots { get; protected set; }
    }
}
