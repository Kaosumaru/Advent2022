using System;

namespace Utils
{
    static public class IEnumerableExt
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            var factorials = Enumerable.Range(0, array.Length + 1)
                .Select(Factorial)
                .ToArray();

            for (var i = 0L; i < factorials[array.Length]; i++)
            {
                var sequence = GenerateSequence(i, array.Length - 1, factorials);

                yield return GeneratePermutation(array, sequence);
            }
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
            {
                Swap(ref clone[i], ref clone[i + sequence[i]]);
            }

            return clone;
        }

        public static IEnumerable<T> RepeatForever<T>(this IEnumerable<T> sequence)
        {
            while(true)
            {
                foreach (var item in sequence)
                    yield return item;
            }
        }

        public static int MultiplyTogether(this IEnumerable<int> enumerable)
        {
            return enumerable.Aggregate(1, (total, next) => total * next);
        }


        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto);
                number = (int)(number % facto);
            }

            return sequence;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private static long Factorial(int n)
        {
            long result = n;

            for (int i = 1; i < n; i++)
            {
                result = result * i;
            }

            return result;
        }

        public static IEnumerable<TItem> ZipAll<TItem>(this IReadOnlyCollection<IEnumerable<TItem>> enumerables)
        {
            var enumerators = enumerables.Select(enumerable => enumerable.GetEnumerator()).ToList();
            bool anyHit;
            do
            {
                anyHit = false;
                foreach (var enumerator in enumerators.Where(enumerator => enumerator.MoveNext()))
                {
                    anyHit = true;
                    yield return enumerator.Current;
                }
            } while (anyHit);

            foreach (var enumerator in enumerators)
            {
                enumerator.Dispose();
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }
    }
}