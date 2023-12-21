using FibonacciHeap;

namespace Utils
{

    public class DjikstraPath
    {
        public class NodeInfo
        {
            public GraphNode Node;
            public float Distance = float.MaxValue;
        }

        public void FindPathTo(GraphNode node)
        {
            TryToSetNewDistance(node, 0);
            StartSearching();
        }

        public void FindPathTo(IEnumerable<GraphNode> nodes)
        {
            foreach (GraphNode node in nodes)
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

        public static float GetDistance(GraphNode from, GraphNode to)
        {
            DjikstraPath djikstra = new();
            djikstra.FindPathTo(to);
            return djikstra.GetDistance(from);
        }

        public float GetDistance(GraphNode node)
        {
            if (_nodes.TryGetValue(node, out var wrapper))
                return wrapper.Distance;
            return float.MaxValue;
        }

        public Dictionary<GraphNode, NodeInfo> GetNodes()
        {
            return _nodes;
        }

        public GraphNode NextNodeOnPath(GraphNode node)
        {
            GraphNode res = null;
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

        public GraphNode PreviousNodeOnPath(GraphNode node)
        {
            GraphNode res = null;
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

        NodeInfo GetWrapper(GraphNode node)
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

        void TryToSetNewDistance(GraphNode node, float distance)
        {
            var wrapper = GetWrapper(node);
            if (wrapper.Distance <= distance)
                return;
            wrapper.Distance = distance;

            // insert into sorted list of nearest nodes
            // TODO we also could remove duplicate entries
            _priority.Insert(new FibonacciHeapNode<NodeInfo, float>(wrapper, distance));
        }


        Dictionary<GraphNode, NodeInfo> _nodes = new();
        FibonacciHeap<NodeInfo, float> _priority = new(0);
    }
}