using System.Text;

namespace Day25
{
    internal class SNAFU
    {
        public SNAFU()
        {
            _data = new();
        }

        public SNAFU(List<int> data)
        {
            _data = data;
        }

        public static SNAFU FromNumber(long n)
        {
            var snafu = new SNAFU(NumberToBase5(n));
            snafu.Normalize();
            return snafu;
        }

        public static SNAFU FromString(string str)
        {
            var numbers = str.Select(CharToNumber).ToList();
            numbers.Reverse();
            return new SNAFU(numbers);
        }

        public static SNAFU operator +(SNAFU a, SNAFU b)
        {
            int count = Math.Max(a._data.Count, b._data.Count);
            List<int> data = new(new int[count]);
            var res = new SNAFU(data);

            for (int i = 0; i < a._data.Count; i++)
                data[i] += a._data[i];

            for (int i = 0; i < b._data.Count; i++)
                data[i] += b._data[i];

            res.Normalize();
            return res;
        }

        public void Normalize()
        {
            int i = 0;
            var data = _data;
            while (true)
            {
                if (i >= data.Count)
                    break;

                var d = data[i];

                if (d >= -2 && d <= 2)
                {
                    i++;
                    continue;
                }


                if (i == data.Count - 1)
                    data.Add(0);

                while (d < -2)
                {
                    d += Base;
                    data[i] = d;
                    data[i + 1]--;
                }

                while (d > 4)
                {
                    d -= Base;
                    data[i] = d;
                    data[i + 1]++;
                }

                if (d == 3 || d == 4)
                {
                    var delta = Base - d;
                    data[i] = -delta;
                    data[i + 1]++;
                }

                i++;
            }
        }

        const int Base = 5;
        public static long ToNumber(string snafu)
        {
            long sum = 0;
            long i = 1;
            foreach (var c in snafu.Reverse())
            {
                sum += CharToNumber(c) * i;
                i *= Base;
            }
            return sum;
        }

        public static int CharToNumber(char c)
        {
            if (c == '-')
                return -1;
            if (c == '=')
                return -2;
            return c - '0';
        }

        static List<int> NumberToBase5(long n)
        {
            List<int> result = new();
            long order = HighestOrder(n);
            while (order > 0)
            {
                long res = n / order;
                result.Add((int)res);
                n -= res * order;

                order /= Base;
            }

            result.Reverse();
            return result;
        }

        static long HighestOrder(long number)
        {
            int result = 1;

            while (number / result > 0)
                result *= Base;

            result /= Base;
            return result;
        }


        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var d in _data.AsEnumerable().Reverse())
            {

                if (d == -2)
                    sb.Append('=');
                else if (d == -1)
                    sb.Append('-');
                else
                    sb.Append(d.ToString());
            }

            return sb.ToString();
        }

        List<int> _data;
    }
}
