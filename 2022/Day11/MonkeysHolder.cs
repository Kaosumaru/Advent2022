using System.Numerics;

namespace Day11
{
    internal class MonkeysHolder
    {

        public bool ShouldDivide = true;

        public void ParseMonkeys(string path)
        {
            using (StreamReader file = new(path))
            {
                while (true)
                {
                    var monkey = Monkey.ParseMonkey(file);
                    if (monkey == null)
                        break;

                    Monkeys.Add(monkey);
                }
            }

            AllMods = Monkeys.Select(m => m.divisior).Aggregate((BigInteger)1, (total, next) => total * next);
        }

        public void Turn()
        {
            foreach (var monkey in Monkeys)
                monkey.Turn(this);
        }

        public Monkey GetMonkey(int i)
        {
            return Monkeys[i];
        }


        public BigInteger AllMods;
        public List<Monkey> Monkeys = new();
    }
}
