namespace Utils
{
    public class GridNode<T> : DjikstraNode
    {
        public GridNode(GenericGrid<T> p, Vector2Int pos) { Parent = p; Position = pos; }

        public T Value;
        public Vector2Int Position { get; protected set; }
        public GenericGrid<T> Parent { get; protected set; }

        public IEnumerable<DjikstraNode.ConnectionInfo> GetConnections()
        {
            return Parent.GetConnectionsFor(Position);
        }
    }

    public interface IGrid<Key, Value>
    {
        public Value GetValue(Key p);
        public void SetValue(Key p, Value v);
    }


    public static class GridExtensions
    {
        public static IEnumerable<Value> ValuesIn<Key, Value>(this IGrid<Key, Value> grid, IEnumerable<Key> positions)
        {
            return positions.Select(p => grid.GetValue(p));
        }
    }
}