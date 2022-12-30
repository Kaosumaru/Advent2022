
using Utils;

class RiskMap : GenericIntGrid
{
    public RiskMap(int w, int h) : base(w, h)
    {

    }

    public override IEnumerable<DjikstraNode.ConnectionInfo> GetConnectionsFor(Vector2Int p)
    {
        int v = GetValue(p);
        return Neighbors4Of(p)
            .Select(n =>
            {
                var node = GetNode(n);
                return new DjikstraNode.ConnectionInfo { Distance = node.Value, Node = node };
            });
    }

}