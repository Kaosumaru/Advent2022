using System.Text;

namespace Day10
{
    public class ParseError : Exception
    {
        public ParseError()
        {
        }

        public ParseError(char token)
        {
            Token = token;
        }

        public char Token { get; set; }
    }

    internal class Parser
    {

        public static Parser? Create(string str)
        {
            Parser parser = new();
            try
            {
                parser.Parse(str);
            }
            catch (ParseError)
            {
                return null;
            }
            return parser;
        }

        public void Parse(string str)
        {
            using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                Parse(reader);
            }
        }

        private void Parse(MemoryStream reader)
        {
            while (true)
            {
                var b = reader.ReadByte();
                if (b == -1)
                    break; // incomplete or done
                char t = (char)b;

                if (IsOpeningToken(t))
                    ParseOpeningToken(t);
                else if (IsClosingToken(t))
                    ParseClosingToken(t);
                else
                    throw new ParseError();
            }
        }

        void ParseOpeningToken(char t)
        {
            Tokens.Push(t);
        }

        void ParseClosingToken(char t)
        {
            if (Tokens.Count == 0)
                throw new ParseError(t);

            var stack = Tokens.Pop();
            if (!DoesClosingTokenMatch(stack, t))
                throw new ParseError(t);
        }

        bool IsOpeningToken(char token)
        {
            return _openingTokens.Contains(token);
        }

        bool IsClosingToken(char token)
        {
            return _closingTokens.Contains(token);
        }

        bool DoesClosingTokenMatch(char stackToken, char token)
        {
            return Array.IndexOf(_openingTokens, stackToken) == Array.IndexOf(_closingTokens, token);
        }

        static char[] _openingTokens = { '[', '(', '{', '<' };
        static char[] _closingTokens = { ']', ')', '}', '>' };
        public Stack<char> Tokens = new();
    }

    static class Scoring
    {
        static Dictionary<char, int> _scores = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        public static int ScoreParse(string str)
        {
            Parser parser = new();
            try
            {
                parser.Parse(str);
            }
            catch (ParseError ex)
            {
                return _scores[ex.Token];
            }
            return 0;
        }

        static Dictionary<char, long> _autocompleteScores = new()
        {
            { '(', 1 },
            { '[', 2 },
            { '{', 3 },
            { '<', 4 },
        };

        public static long ScoreAutocomplete(Parser parser)
        {
            long score = 0;
            foreach (var token in parser.Tokens)
            {
                score *= 5;
                score += _autocompleteScores[token];
            }

            return score;
        }
    }
}
