using System.Globalization;

namespace Utils
{
    public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static public int ManhattanDistance(Vector2Int a, Vector2Int b)
        {
            var dx = Math.Abs(b.x - a.x);
            var dy = Math.Abs(b.y - a.y);
            return dx + dy;
        }

        public static Vector2Int operator -(Vector2Int v)
        {
            return new Vector2Int(-v.x, -v.y);
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }

        public static Vector2Int operator *(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y)); }

        // Returns a vector that is made from the largest components of two vectors.

        public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y)); }

        public Vector2Int Sign()
        {
            return new(Math.Sign(x), Math.Sign(y));
        }

        public static Vector2Int operator *(int a, Vector2Int b)
        {
            return new Vector2Int(a * b.x, a * b.y);
        }

        public static Vector2Int operator *(Vector2Int a, int b)
        {
            return new Vector2Int(a.x * b, a.y * b);
        }

        public static Vector2Int operator /(Vector2Int a, int b)
        {
            return new Vector2Int(a.x / b, a.y / b);
        }

        public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? other)
        {
            if (other is not Vector2Int) return false;

            return Equals((Vector2Int)other);
        }

        public bool Equals(Vector2Int other)
        {
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return string.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        }

        public static Vector2Int Zero { get { return s_Zero; } }
        public static Vector2Int One { get { return s_One; } }
        public static Vector2Int Up { get { return s_Up; } }
        public static Vector2Int Down { get { return s_Down; } }
        public static Vector2Int Left { get { return s_Left; } }
        public static Vector2Int Right { get { return s_Right; } }

        private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);
        private static readonly Vector2Int s_One = new Vector2Int(1, 1);
        private static readonly Vector2Int s_Up = new Vector2Int(0, 1);
        private static readonly Vector2Int s_Down = new Vector2Int(0, -1);
        private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);
        private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
    }



    public static class Vector2IntExtensions
    {
        public static (Vector2Int, Vector2Int) Bounds(this IEnumerable<Vector2Int> collection)
        {
            Vector2Int boundsMin = new(int.MaxValue, int.MaxValue);
            Vector2Int boundsMax = new(int.MinValue, int.MinValue);

            foreach (var entry in collection)
            {
                boundsMin = Vector2Int.Min(boundsMin, entry);
                boundsMax = Vector2Int.Max(boundsMax, entry);
            }

            return (boundsMin, boundsMax);
        }

        public static IEnumerable<Vector2Int> Between(Vector2Int start, Vector2Int end)
        {
            for (int y = start.y; y <= end.y; y++)
                for (int x = start.x; x <= end.x; x++)
                    yield return new(x, y);
        }

        public static IEnumerable<Vector2Int> Neighbors4Of(Vector2Int p)
        {
            foreach (var d in Directions)
            {
                Vector2Int c = p + d;
                yield return c;
            }
        }

        public static IEnumerable<Vector2Int> Neighbors4AndSelf(Vector2Int p)
        {
            yield return p;
            foreach (var d in Directions)
            {
                Vector2Int c = p + d;
                yield return c;
            }
        }

        public static Vector2Int[] Directions = new Vector2Int[] {
            new Vector2Int{ x = 1, y = 0},
            new Vector2Int{ x = -1, y = 0},
            new Vector2Int{ x = 0, y = 1},
            new Vector2Int{ x = 0, y = -1},
        };
    }
}