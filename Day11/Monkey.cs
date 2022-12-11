using Day11.Tokens;
using System.Numerics;

namespace Day11
{
    internal class Monkey
    {
        public static Monkey? ParseMonkey(StreamReader file)
        {
            var monkeyId = file.ReadLine();
            if (monkeyId == null)
                return null;

            Monkey monkey = new();

            var startingItems = file.ReadLine() ?? "";
            monkey.heldItems = startingItems
                .Split(": ")[1]
                .Split(", ")
                .Select(str => BigInteger.Parse(str))
                .ToList();

            var operation = file.ReadLine() ?? "";
            monkey.SetOperation(operation.Split("new = ")[1]);

            var test = file.ReadLine() ?? "";
            monkey.divisior = BigInteger.Parse(test.Split("divisible by ")[1]);

            var ifTrue = file.ReadLine() ?? "";
            monkey.ifTrue = int.Parse(ifTrue.Split("throw to monkey ")[1]);

            var ifFalse = file.ReadLine() ?? "";
            monkey.ifFalse = int.Parse(ifFalse.Split("throw to monkey ")[1]);

            // empty line
            file.ReadLine();

            return monkey;
        }

        void SetOperation(string str)
        {
            var tokens = str.Split(' ');
            Token t1, t2;

            t1 = StrToTokenValue(tokens[0]);
            t2 = StrToTokenValue(tokens[2]);

            var o = tokens[1];
            if (o == "+")
                operation = new Addition(t1, t2);
            else if (o == "*")
                operation = new Multiplication(t1, t2);
        }

        Token StrToTokenValue(string str)
        {
            if (int.TryParse(str, out int result))
                return new Constant(result);

            if (str == "old")
                return new Global();

            return new Token();
        }

        public void Turn(MonkeysHolder context)
        {
            int c = heldItems.Count;
            for (int i = 0; i < heldItems.Count; i++)
            {
                var newItem = InspectItem(heldItems[i], context);
                if (ShouldThrowToFirst(newItem))
                {
                    context.GetMonkey(ifTrue).heldItems.Add(newItem);
                }
                else
                    context.GetMonkey(ifFalse).heldItems.Add(newItem);
            }
            heldItems.RemoveRange(0, c);
        }

        BigInteger InspectItem(BigInteger old, MonkeysHolder context)
        {
            InspectCount++;
            Global.Value = old;
            var n = operation.Calculate();

            if (context.ShouldDivide)
                n /= 3;
            else
                n %= context.AllMods;
            return n;
        }

        bool ShouldThrowToFirst(BigInteger value)
        {
            return value % divisior == 0;
        }

        List<BigInteger> heldItems;
        Token operation;
        public BigInteger divisior;
        int ifTrue;
        int ifFalse;

        public BigInteger InspectCount { get; protected set; }
    }
}
