namespace Utils
{
    public interface GraphNode
    {
        struct ConnectionInfo
        {
            public GraphNode Node;
            public float Distance;
        }

        IEnumerable<ConnectionInfo> GetConnections();
    }
}