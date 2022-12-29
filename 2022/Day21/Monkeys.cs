using Utils;

namespace Day21
{
    internal class Monkeys
    {
        public Monkeys(bool equality)
        {
            _equality = equality;
        }

        bool _equality = false;

        public void AddLine(string id, string operation)
        {
            AddToken(id, CreateTokenForOperation(id, operation));
        }

        Token CreateTokenForOperation(string id, string operation)
        {
            Token tk = TryToAddConstant(id, operation);
            if (tk != null)
                return tk;
            return TryToAddBinaryOperation(id, operation);
        }

        public Token TryToAddConstant(string id, string number)
        {
            if (_equality && id == "humn")
                return new Unknown();

            if (!long.TryParse(number, out var value))
                return null;

            return new Constant(value);
        }

        public Token TryToAddBinaryOperation(string id, string operation)
        {
            var s = operation.Split(' ');
            var token1 = TryToGetToken(s[0]);
            var token2 = TryToGetToken(s[2]);
            var op = s[1][0];

            if (_equality && id == "root")
                return new EqualsToken(token1, token2);

            return BinaryOperation.Create(token1, token2, op);
        }

        public void AddToken(string id, Token token)
        {
            if (_idToToken.TryGetValue(id, out var proxyToken))
            {
                var proxy = proxyToken as ProxyToken;
                proxy.SetToken(token);
                return;
            }

            _idToToken[id] = token;
        }

        public Token TryToGetToken(string id)
        {
            if (_idToToken.TryGetValue(id, out var token))
                return token;

            var t = new ProxyToken();
            _idToToken[id] = t;
            return t;
        }

        public long CalculateFor(string id)
        {
            var t = TryToGetToken(id);
            return t.Calculate();
        }

        public long SolveFor(string id)
        {
            var t = TryToGetToken(id);
            return t.Solve(0);
        }

        public class ProxyToken : Token
        {
            public void SetToken(Token t)
            {
                _token = t;
            }

            public override long Calculate()
            {
                return _token.Calculate();
            }

            public override bool IsKnown() { return _token.IsKnown(); }

            public override long Solve(long eq)
            {
                return _token.Solve(eq);
            }


            Token _token;
        }

        public class EqualsToken : BinaryOperation
        {
            public EqualsToken(Token t1, Token t2) : base(t1, t2)
            {
            }
            public override long Calculate()
            {
                return _t1.Calculate() == _t2.Calculate() ? 1 : 0;
            }


            public override long SolveValueForToken1(long eq, long value)
            {
                return value;
            }

            public override long SolveValueForToken2(long eq, long value)
            {
                return value;
            }
        }

        Dictionary<string, Token> _idToToken = new();
    }
}
