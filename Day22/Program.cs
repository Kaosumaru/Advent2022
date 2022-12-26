using Day22;
using Utils;



(GenericGrid<int>, Orders) CreateGridFromFile(List<string> lines)
{
    var orderLine = lines.Last();

    lines.RemoveAt(lines.Count - 1);
    lines.RemoveAt(lines.Count - 1);

    int w = lines.Select(l => l.Length).Max();
    int h = lines.Count;

    var grid = new GenericGrid<int>(w, h);

    foreach (var p in grid.AllPositions())
    {
        var lineH = lines[p.y];

        int v = 2;

        if (p.x < lineH.Length)
        {
            int c = lineH[p.x];
            if (c == '.')
                v = 0;
            else if (c == '#')
                v = 1;
        }

        grid.SetValue(p, v);
    }

    var orders = new Orders();
    orders.Parse(orderLine);

    return (grid, orders);
}


void Exercise1(GenericGrid<int> grid, Orders orders)
{
    TraversalGrid trav = new TraversalGrid(grid);
    trav.Traverse(orders);
    // grid.Display((d) => d.ToString());

    int result = (trav.Position.y + 1) * 1000 + (trav.Position.x + 1) * 4 + trav.Direction;
    Console.WriteLine(result);
}

void Exercise2(GenericGrid<int> grid, Orders orders, int faceSize)
{
    var trav = new TraversalCube(grid, faceSize);
    trav.Traverse(orders);
    int result = (trav.Position.y + 1) * 1000 + (trav.Position.x + 1) * 4 + trav.Direction;
    Console.WriteLine(result);
}


#if false
string path = "../../../data.txt";
int faceSize = 4;
int[] faceIndexes = new int[]
{
    0, 0, 1, 0,
    2, 3, 5, 0,
    0, 0, 6, 4,
};
#else
string path = "../../../data2.txt";
int faceSize = 50;
int[] faceIndexes = new int[]
{
    0, 1, 4,
    0, 5, 0,
    3, 6, 0,
    2, 0, 0,
};

int[] faceIndexes2 = new int[]
{
    0, 1, 2,
    0, 4, 0,
    6, 7, 0,
    9, 0, 0,
};

#endif

var lines = File.ReadAllLines(path).ToList();
var (grid, orders) = CreateGridFromFile(lines);
Exercise2(grid, orders, faceSize);
