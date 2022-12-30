using FibonacciHeap;

namespace Utils
{

    public interface DjikstraNode
    {
        struct ConnectionInfo
        {
            public DjikstraNode Node;
            public float Distance;
        }

        IEnumerable<ConnectionInfo> GetConnections();
    }

    public class DjikstraPath
    {
        public class NodeInfo
        {
            public DjikstraNode Node;
            public float Distance = float.MaxValue;
        }

        public void FindPathTo(DjikstraNode node)
        {
            TryToSetNewDistance(node, 0);
            StartSearching();
        }

        public void FindPathTo(IEnumerable<DjikstraNode> nodes)
        {
            foreach (DjikstraNode node in nodes)
                TryToSetNewDistance(node, 0);
            StartSearching();
        }

        void StartSearching()
        {
            while (!_priority.IsEmpty())
            {
                var top = _priority.RemoveMin().Data;
                UpdateNeighbors(top);
            }
        }

        public float GetDistance(DjikstraNode node)
        {
            if (_nodes.TryGetValue(node, out var wrapper))
                return wrapper.Distance;
            return float.MaxValue;
        }

        public Dictionary<DjikstraNode, NodeInfo> GetNodes()
        {
            return _nodes;
        }

        public DjikstraNode NextNodeOnPath(DjikstraNode node)
        {
            DjikstraNode res = null;
            float distance = GetDistance(node);
            foreach (var neighbor in node.GetConnections())
            {
                var neighborDistance = GetDistance(neighbor.Node);
                if (distance <= neighborDistance)
                    continue;
                distance = neighborDistance;
                res = neighbor.Node;
            }

            return res;
        }

        public DjikstraNode PreviousNodeOnPath(DjikstraNode node)
        {
            DjikstraNode res = null;
            float distance = GetDistance(node);
            foreach (var neighbor in node.GetConnections())
            {
                var neighborDistance = GetDistance(neighbor.Node);
                if (distance > neighborDistance)
                    continue;
                distance = neighborDistance;
                res = neighbor.Node;
            }

            return res;
        }

        NodeInfo GetWrapper(DjikstraNode node)
        {
            if (_nodes.TryGetValue(node, out var wrapper))
                return wrapper;
            wrapper = new();
            wrapper.Node = node;
            _nodes[node] = wrapper;
            return wrapper;
        }

        void UpdateNeighbors(NodeInfo node)
        {
            var connections = node.Node.GetConnections();
            foreach (var connection in connections)
            {
                TryToSetNewDistance(connection.Node, node.Distance + connection.Distance);
            }
        }

        void TryToSetNewDistance(DjikstraNode node, float distance)
        {
            var wrapper = GetWrapper(node);
            if (wrapper.Distance <= distance)
                return;
            wrapper.Distance = distance;

            // insert into sorted list of nearest nodes
            // TODO we also could remove duplicate entries
            _priority.Insert(new FibonacciHeapNode<NodeInfo, float>(wrapper, distance));
        }


        Dictionary<DjikstraNode, NodeInfo> _nodes = new();
        FibonacciHeap<NodeInfo, float> _priority = new(0);
    }
}