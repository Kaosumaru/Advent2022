namespace Day20
{
    public class NumberEntry
    {
        public int Value;


        public NumberEntry Move(int elements)
        {
            NumberEntry entry = this;
            if (elements < 0)
            {
                for (int i = 0; i <= -elements; i++)
                {
                    entry = entry.Prev;

                    // hack for wrapping
                    if (entry == this)
                        i--;
                }

            }
            else if (elements > 0)
            {
                for (int i = 0; i < elements; i++)
                {
                    entry = entry.Next;

                    // hack for wrapping
                    if (entry == this)
                        i--;
                }

            }

            return entry;
        }

        public NumberEntry Next;
        public NumberEntry Prev;

    }

}
