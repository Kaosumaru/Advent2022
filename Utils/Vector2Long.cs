using System.Globalization;

namespace Utils
{
    public struct Vector2Long : IEquatable<Vector2Long>, IFormattable
    {
        public long x;
        public long y;

        public Vector2Long(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        static public long ManhattanDistance(Vector2Long a, Vector2Long b)
        {
            var dx = Math.Abs(b.x - a.x);
            var dy = Math.Abs(b.y - a.y);
            return dx + dy;
        }

        public static Vector2Long operator -(Vector2Long v)
        {
            return new Vector2Long(-v.x, -v.y);
        }

        public static Vector2Long operator +(Vector2Long a, Vector2Long b)
        {
            return new Vector2Long(a.x + b.x, a.y + b.y);
        }

        public static Vector2Long operator -(Vector2Long a, Vector2Long b)
        {
            return new Vector2Long(a.x - b.x, a.y - b.y);
        }

        public static Vector2Long operator *(Vector2Long a, Vector2Long b)
        {
            return new Vector2Long(a.x * b.x, a.y * b.y);
        }

        public Vector2Long Sign()
        {
            return new(Math.Sign(x), Math.Sign(y));
        }

        public static Vector2Long operator *(long a, Vector2Long b)
        {
            return new Vector2Long(a * b.x, a * b.y);
        }

        public static Vector2Long operator *(Vector2Long a, long b)
        {
            return new Vector2Long(a.x * b, a.y * b);
        }

        public static Vector2Long operator /(Vector2Long a, long b)
        {
            return new Vector2Long(a.x / b, a.y / b);
        }

        public static bool operator ==(Vector2Long lhs, Vector2Long rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Vector2Long lhs, Vector2Long rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? other)
        {
            if (other is not Vector2Long) return false;

            return Equals((Vector2Long)other);
        }

        public bool Equals(Vector2Long other)
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

        public static Vector2Long Zero { get { return s_Zero; } }
        public static Vector2Long One { get { return s_One; } }
        public static Vector2Long Up { get { return s_Up; } }
        public static Vector2Long Down { get { return s_Down; } }
        public static Vector2Long Left { get { return s_Left; } }
        public static Vector2Long Right { get { return s_Right; } }

        private static readonly Vector2Long s_Zero = new Vector2Long(0, 0);
        private static readonly Vector2Long s_One = new Vector2Long(1, 1);
        private static readonly Vector2Long s_Up = new Vector2Long(0, 1);
        private static readonly Vector2Long s_Down = new Vector2Long(0, -1);
        private static readonly Vector2Long s_Left = new Vector2Long(-1, 0);
        private static readonly Vector2Long s_Right = new Vector2Long(1, 0);
    }
}