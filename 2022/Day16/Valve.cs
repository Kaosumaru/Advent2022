using Utils;

namespace Day16
{
    internal class Valve : GraphNode
    {
        public Valve(string id, Valves parent)
        {
            Id = id;
            _parent = parent;
        }

        public string Id;
        public List<Valve> Connections = new();
        public int Flow;
        public Valves _parent;

        public IEnumerable<GraphNode.ConnectionInfo> GetConnections()
        {
            foreach (var conn in Connections)
                yield return new GraphNode.ConnectionInfo { Distance = 1, Node = conn };
        }
    }
}
