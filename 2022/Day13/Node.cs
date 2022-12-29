namespace Day13
{
    internal class Node : IComparable<Node>
    {
        public enum NodeType
        {
            Value,
            List
        }

        public Node(NodeType type, int v = 0)
        {
            Type = type;
            Value = v;
        }

        public int Value { get; protected set; }
        public int Metadata { get; set; }
        public List<Node> Children { get; protected set; } = new List<Node>();
        public NodeType Type { get; protected set; }


        public static Node CreateValue(int v)
        {
            return new Node(NodeType.Value, v);
        }

        public static Node CreateList()
        {
            return new Node(NodeType.List);
        }

        public int CompareTo(Node? other)
        {
            if (other == null)
                return 1;

            if (other.Type == NodeType.Value && Type == NodeType.Value)
                return Value.CompareTo(other.Value);


            return CompareLists(ConvertToEnumerable(), other.ConvertToEnumerable());
        }

        // will return 1 if a is greater, 0 if they are equal, -1 if b is greater
        static int CompareLists(IEnumerable<Node> a, IEnumerable<Node> b)
        {
            foreach (var pair in a.Zip(b))
            {
                var c = pair.First.CompareTo(pair.Second);
                if (c != 0)
                    return c;
            }

            var ac = a.Count();
            var bc = b.Count();

            // return which sequence is larger
            return Math.Sign(ac - bc);
        }

        IEnumerable<Node> ConvertToEnumerable()
        {
            if (Type == NodeType.Value)
                return Enumerable.Repeat(this, 1);

            return Children;
        }

        // Define the is greater than operator.
        public static bool operator >(Node operand1, Node operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        // Define the is less than operator.
        public static bool operator <(Node operand1, Node operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }
    }
}
