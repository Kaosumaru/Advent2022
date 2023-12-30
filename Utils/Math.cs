namespace Utils
{
    static public class MathUtils
    {
        public static int DivideUp(int a, int b)
        {
            return (a - 1) / b + 1;
        }

        public static int WrapAround(int v, int maxval)
        {
            if (v < 0)
                return (maxval + (v % maxval)) % maxval;
            return v % maxval;
        }

        public static int Gfc(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long Gfc(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static int Lcm(int a, int b)
        {
            return (a / Gfc(a, b)) * b;
        }



        public static int Lcm(this IEnumerable<int> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / Gfc(S, val));
        }

        public static long Lcm(this IEnumerable<long> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / Gfc(S, val));
        }
    }
}