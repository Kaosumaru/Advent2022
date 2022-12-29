namespace Day20
{
    public class Numbers
    {
        public Numbers()
        {

        }

        public void Add(long n)
        {
            var number = new NumberEntry();
            number.Value = n;

            _numbers.Add(number);

            if (n == 0)
                _zero = number;
        }

        public void Finish()
        {
            foreach (var pair in _numbers.Zip(_numbers.Skip(1)))
            {
                pair.First.Next = pair.Second;
                pair.Second.Prev = pair.First;
            }

            var l = _numbers[_numbers.Count - 1];
            var f = _numbers[0];

            f.Prev = l;
            l.Next = f;
        }

        public long Get(int pos)
        {
            var t = _zero.Move(pos % _numbers.Count);
            return t.Value;
        }

        public long Result()
        {
            return Get(1000) + Get(2000) + Get(3000);
        }

        public void Mix()
        {
            foreach (var n in _numbers)
            {
                MoveElement(n);
            }
        }

        void MoveElement(NumberEntry entry)
        {
            long v = entry.Value;
            if (v == 0)
                return;

            NumberEntry start = entry.Prev;
            RemoveElement(entry);

            v %= _numbers.Count - 1;
            var target = start.Move(v);
            InsertElement(entry, target);
        }


        void RemoveElement(NumberEntry entry)
        {
            var next = entry.Next;
            var prev = entry.Prev;

            prev.Next = next;
            next.Prev = prev;

            entry.Next = null;
            entry.Prev = null;
        }

        void InsertElement(NumberEntry entry, NumberEntry after)
        {
            var prev = after;
            var next = after.Next;

            entry.Next = next;
            entry.Prev = prev;

            next.Prev = entry;
            prev.Next = entry;

        }

        void Display()
        {
            var first = _zero;
            var current = first;
            while (true)
            {
                Console.Write($"{current.Value}, ");
                current = current.Next;
                if (current == first)
                    break;
            }
            Console.WriteLine();
        }

        public List<NumberEntry> _numbers = new();
        NumberEntry _zero;
    }

}
