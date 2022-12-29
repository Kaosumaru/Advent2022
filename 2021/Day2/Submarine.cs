using Utils;

namespace Day2
{
    internal class Submarine
    {
        public void ParseLine(string line)
        {
            var s = line.Split(' ');
            var command = s[0];
            var amount = int.Parse(s[1]);

            Vector2Int dir = Vector2Int.Zero;
            if (command == "up")
                dir = Vector2Int.Down;
            else if (command == "down")
                dir = Vector2Int.Up;
            else if (command == "forward")
                dir = Vector2Int.Right;

            Move(dir, amount);
        }

        void Move(Vector2Int direction, int amount)
        {
            Position += direction * amount;
        }

        public Vector2Int Position;
    }

    internal class Submarine2
    {
        public void ParseLine(string line)
        {
            var s = line.Split(' ');
            var command = s[0];
            var amount = int.Parse(s[1]);

            Vector2Int dir = Vector2Int.Zero;
            if (command == "up")
                AdjustAim(-amount);
            else if (command == "down")
                AdjustAim(amount);
            else if (command == "forward")
                Forward(amount);
        }

        void AdjustAim(int amount)
        {
            Aim += amount;
        }

        void Forward(int amount)
        {
            Position += Vector2Int.Right * amount;
            Position += Vector2Int.Up * Aim * amount;
        }

        public int Aim;
        public Vector2Int Position;
    }
}
