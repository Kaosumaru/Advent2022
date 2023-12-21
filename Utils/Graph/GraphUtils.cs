namespace Utils
{
    public class GraphUtils
    {
        static public IEnumerable<GraphNode> IterateAllNodesRecursively(GraphNode node)
        {
            var list = node.GetConnections().ToList();

            while(list.Count > 0)
            {
                var item = list.Last();
                list.RemoveAt(list.Count - 1);

                yield return item.Node;
                list.AddRange(item.Node.GetConnections());
            }
        }
    }
}