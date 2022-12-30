
using Utils;

class HeightMap : GenericIntGrid
{
    public HeightMap(int w, int h) : base(w, h)
    {

    }

    public IEnumerable<Vector2Int> Lowpoints()
    {
        return AllPositions().Where(IsLowpoint);
    }

    bool IsLowpoint(Vector2Int p)
    {
        int v = GetValue(p);
        return Neighbors4Of(p).All(n => GetValue(n) > v);
    }

    public int FloodFill(Vector2Int p)
    {
        DjikstraPath djikstra = new();
        djikstra.FindPathTo(GetNode(p));
        return djikstra.GetNodes().Count;
    }


    // we can resue djikstra for flood fill
    public override IEnumerable<DjikstraNode.ConnectionInfo> GetConnectionsFor(Vector2Int p)
    {
        int v = GetValue(p);
        return Neighbors4Of(p)
            .Where(n =>
            {
                int nv = GetValue(n);
                return nv != 9 && nv > v;
            })
            .Select(n => new DjikstraNode.ConnectionInfo { Distance = 1, Node = GetNode(n) });
    }

}