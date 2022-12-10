using Utils;

namespace Day10
{
    internal class CRT
    {
        public void ParseFile(string path)
        {
            foreach (var line in File.ReadLines(path))
                ParseLine(line);
        }

        public void Display()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write(Pixels[x, y] ? '#' : '.');
                Console.WriteLine();
            }
        }

        void ParseLine(string line)
        {
            var s = line.Split(" ");
            var c = s[0];

            if (c == "noop")
                Noop();
            else if (c == "addx")
                AddX(int.Parse(s[1]));
        }

        void AddX(int a)
        {
            IncreaseTime(2);
            X += a;
        }

        void Noop()
        {
            IncreaseTime(1);
        }

        void IncreaseTime(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Time++;
                SetCurrentPixel();
                OnTimeElapsed?.Invoke();
            }
        }

        void SetCurrentPixel()
        {
            var p = CurrentDrawPosition();
            Pixels[p.x, p.y] = ShouldPixelBeLit(p);
        }

        Vector2Int CurrentDrawPosition()
        {
            var p = (Time - 1) % (Width * Height);
            Vector2Int v;
            v.x = p % Width;
            v.y = p / Width;
            return v;
        }

        bool ShouldPixelBeLit(Vector2Int pixel)
        {
            return pixel.x >= X - 1 && pixel.x <= X + 1;
        }

        public const int Width = 40;
        public const int Height = 6;
        public bool[,] Pixels = new bool[Width, Height];

        public int X { get; protected set; } = 1;
        public int Time { get; protected set; } = 0;

        public Action? OnTimeElapsed;
    }
}
