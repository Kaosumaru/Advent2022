using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utils.Graph
{

    public class SimpleNode : GraphNode
    {
        public void AddConnection(GraphNode.ConnectionInfo info)
        {
            _connections.Add(info);
        }

        public void AddConnection(SimpleNode node)
        {
            _connections.Add(new GraphNode.ConnectionInfo { Distance = 1, Node = node });
        }

        public IEnumerable<GraphNode.ConnectionInfo> GetConnections()
        {
            return _connections;
        }

        public List<GraphNode.ConnectionInfo> GetConnectionList()
        {
            return _connections;
        }

        public object id() { return _id; }

        object _id;
        List<GraphNode.ConnectionInfo> _connections = new();

        internal void SetId(object id)
        {
            _id = id;
        }
    }

    public class SimpleGraph<IDType, NodeType> 
        where IDType : class 
        where NodeType : SimpleNode, new()
    {
        public NodeType GetNode(IDType id)
        {
            if (_nodes.TryGetValue(id, out var node))
                return node;
            node = new NodeType();
            node.SetId(id);
            _nodes[id] = node;
            return node;
        }

        public Dictionary<IDType, NodeType> GetNodes() {  return _nodes; }

        Dictionary<IDType, NodeType> _nodes = new();
    }
}
