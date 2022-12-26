namespace Utils
{
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