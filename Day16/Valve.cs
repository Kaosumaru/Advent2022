using Utils;

namespace Day16
{
    internal class Valve : DjikstraNode
    {
        public Valve(string id, Valves parent)
        {
            Id = id;
            _parent = parent;
        }

        public string Id;
        public List<Valve> Connections = new();
        public int Flow;
        Valves _parent;

        public IEnumerable<DjikstraNode.ConnectionInfo> GetConnections()
        {
            foreach (var conn in Connections)
                yield return new DjikstraNode.ConnectionInfo { Distance = 1, Node = conn };
        }
    }
}
