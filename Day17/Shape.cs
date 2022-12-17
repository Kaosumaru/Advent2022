using Utils;

namespace Day17
{
    internal class Shape
    {
        public Shape(int[][] arr)
        {
            Width = arr[0].Length;
            Height = arr.Length;

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (arr[y][x] == 1)
                        Entries.Add(new(x, -y));
        }

        public IEnumerable<Vector2Long> PointsWithOffset(Vector2Long offset)
        {
            return Entries.Select(p => p + offset);
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public List<Vector2Long> Entries { get; protected set; } = new();

        public static List<Shape> Shapes()
        {
            int[][][] shapes = new int[][][]
            {
                new int[][] {
                    new int[]{ 1, 1, 1, 1 }
                },

                new int[][] {
                    new int[]{ 0, 1, 0 },
                    new int[]{ 1, 1, 1 },
                    new int[]{ 0, 1, 0 },
                },

                new int[][] {
                    new int[]{ 0, 0, 1 },
                    new int[]{ 0, 0, 1 },
                    new int[]{ 1, 1, 1 },
                },

                new int[][] {
                    new int[]{ 1 },
                    new int[]{ 1 },
                    new int[]{ 1 },
                    new int[]{ 1 },
                },

                new int[][] {
                    new int[]{ 1, 1 },
                    new int[]{ 1, 1 },
                }
            };

            return shapes.Select(e => new Shape(e)).ToList();
        }

    }
}

