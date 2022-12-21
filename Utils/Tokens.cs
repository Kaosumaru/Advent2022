namespace Utils
{
    public class Token
    {
        public virtual bool IsKnown() { return true; }
        public virtual long Calculate()
        {
            return 0;
        }

        public virtual long Solve(long eq)
        {
            throw new ArithmeticException();
        }
    }

    public class Constant : Token
    {
        public Constant(long c) { _value = c; }

        public override long Calculate()
        {
            return _value;
        }

        long _value;
    }

    public class Global : Token
    {
        public Global() { }

        public override long Calculate()
        {
            return Value;
        }

        public static long Value;
    }

    public class BinaryOperation : Token
    {
        public BinaryOperation(Token t1, Token t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        public void SetLeftToken(Token t)
        {
            _t1 = t;
        }

        public void SetRightToken(Token t)
        {
            _t2 = t;
        }

        protected Token _t1, _t2;

        static public BinaryOperation Create(Token t1, Token t2, char op)
        {
            switch (op)
            {
                case '+':
                    return new Addition(t1, t2);
                case '-':
                    return new Substraction(t1, t2);
                case '*':
                    return new Multiplication(t1, t2);
                case '/':
                    return new Division(t1, t2);
            }
            return null;
        }

        public override long Solve(long eq)
        {
            var t1K = _t1.IsKnown();
            var t2K = _t2.IsKnown();

            if (!t1K && !t2K)
                throw new ArithmeticException();
            if (!t1K)
                return _t1.Solve(SolveValueForToken1(eq, _t2.Calculate()));
            if (!t2K)
                return _t2.Solve(SolveValueForToken2(eq, _t1.Calculate()));

            throw new ArithmeticException();
        }

        public virtual long SolveValueForToken1(long eq, long value)
        {
            return 0;
        }

        public virtual long SolveValueForToken2(long eq, long value)
        {
            return 0;
        }

        public override bool IsKnown()
        {
            return _t1.IsKnown() && _t2.IsKnown();
        }
    }

    public class Addition : BinaryOperation
    {
        public Addition(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override long Calculate()
        {
            return _t1.Calculate() + _t2.Calculate();
        }

        public override long SolveValueForToken1(long eq, long value)
        {
            return eq - value;
        }

        public override long SolveValueForToken2(long eq, long value)
        {
            return eq - value;
        }
    }

    public class Substraction : BinaryOperation
    {
        public Substraction(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override long Calculate()
        {
            return _t1.Calculate() - _t2.Calculate();
        }

        public override long SolveValueForToken1(long eq, long value)
        {
            return eq + value;
        }

        public override long SolveValueForToken2(long eq, long value)
        {
            return value - eq;
        }
    }

    public class Multiplication : BinaryOperation
    {
        public Multiplication(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override long Calculate()
        {
            return _t1.Calculate() * _t2.Calculate();
        }

        public override long SolveValueForToken1(long eq, long value)
        {
            return eq / value;
        }

        public override long SolveValueForToken2(long eq, long value)
        {
            return eq / value;
        }
    }

    public class Division : BinaryOperation
    {
        public Division(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override long Calculate()
        {
            return _t1.Calculate() / _t2.Calculate();
        }

        public override long SolveValueForToken1(long eq, long value)
        {
            return eq * value;
        }

        public override long SolveValueForToken2(long eq, long value)
        {
            return value / eq;
        }
    }

    public class Unknown : Token
    {
        public override bool IsKnown()
        {
            return false;
        }

        public Unknown()
        {
        }

        public override long Calculate()
        {
            return 0;
        }

        public override long Solve(long eq)
        {
            return eq;
        }
    }
}
