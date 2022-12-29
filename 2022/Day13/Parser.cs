using System.Text;

namespace Day13
{
    internal class Parser
    {
        public Parser(string str)
        {
            using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                Parse(reader);
            }
        }

        void Parse(MemoryStream r)
        {
            while (true)
            {
                int b = r.ReadByte();
                if (b == -1)
                    break;

                if (b == ',')
                {
                    // NOOP
                }
                else if (b == '[')
                    OnListOpen();
                else if (b == ']')
                    OnListClose();
                else if (b >= '0' && b <= '9')
                {
                    r.Seek(-1, SeekOrigin.Current);
                    ParseNumber(r);
                }
            }
        }

        private void ParseNumber(MemoryStream r)
        {
            long start = r.Position;
            while (true)
            {
                int b = r.ReadByte();
                if (b == -1)
                    break;
                if (b < '0' || b > '9')
                    break;
            }

            int length = (int)(r.Position - start - 1);
            r.Seek(start, SeekOrigin.Begin);


            byte[] buffer = new byte[length];
            Span<byte> bytes = buffer;

            r.Read(bytes);

            var str = Encoding.ASCII.GetString(buffer);
            int i = int.Parse(str);
            OnValue(i);
        }

        public Node RootNode()
        {
            return _rootNode;
        }

        public void OnListOpen()
        {
            var list = Node.CreateList();
            if (_stack.Count == 0)
            {
                _rootNode = list;
                _stack.Push(list);
                return;
            }

            _stack.Peek().Children.Add(list);
            _stack.Push(list);
        }

        public void OnListClose()
        {
            _stack.Pop();
        }

        public void OnValue(int v)
        {
            if (_stack.Count == 0)
            {
                // not implemented
                return;
            }

            _stack.Peek().Children.Add(Node.CreateValue(v));
        }

        Node _rootNode;
        Stack<Node> _stack = new();
    }
}
