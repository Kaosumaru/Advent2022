namespace Day20
{
    public class Numbers
    {
        public Numbers()
        {

        }

        public void Add(int n)
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

        public int Get(int pos)
        {
            var t = _zero.Move(pos % _numbers.Count);
            return t.Value;
        }


        public void MoveAllElements()
        {
            foreach (var n in _numbers)
            {
                MoveElement(n);
            }
        }

        void MoveElement(NumberEntry entry)
        {
            int v = entry.Value;
            if (v == 0)
                return;


            var target = entry.Move(v);

            // todo check if should be earlier
            RemoveElement(entry);

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
            var first = _numbers[0];
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
