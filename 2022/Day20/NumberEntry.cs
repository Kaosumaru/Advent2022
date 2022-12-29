namespace Day20
{
    public class NumberEntry
    {
        public long Value;


        public NumberEntry Move(long elements)
        {
            NumberEntry entry = this;
            if (elements < 0)
            {
                for (long i = 0; i < -elements; i++)
                {
                    entry = entry.Prev;
                }

            }
            else if (elements > 0)
            {
                for (long i = 0; i < elements; i++)
                {
                    entry = entry.Next;
                }

            }

            return entry;
        }

        public NumberEntry Next;
        public NumberEntry Prev;

    }

}
