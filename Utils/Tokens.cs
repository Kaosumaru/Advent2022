using System.Numerics;

namespace Utils
{
    internal class Token
    {
        public virtual BigInteger Calculate()
        {
            return 0;
        }
    }

    internal class Constant : Token
    {
        public Constant(BigInteger c) { _value = c; }

        public override BigInteger Calculate()
        {
            return _value;
        }

        BigInteger _value;
    }

    internal class Global : Token
    {
        public Global() { }

        public override BigInteger Calculate()
        {
            return Value;
        }

        public static BigInteger Value;
    }

    internal class BinaryOperation : Token
    {
        public BinaryOperation(Token t1, Token t2)
        {
            _t1 = t1;
            _t2 = t2;
        }

        protected Token _t1, _t2;
    }

    internal class Addition : BinaryOperation
    {
        public Addition(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override BigInteger Calculate()
        {
            return _t1.Calculate() + _t2.Calculate();
        }
    }

    internal class Multiplication : BinaryOperation
    {
        public Multiplication(Token t1, Token t2) : base(t1, t2)
        {
        }

        public override BigInteger Calculate()
        {
            return _t1.Calculate() * _t2.Calculate();
        }
    }
}
