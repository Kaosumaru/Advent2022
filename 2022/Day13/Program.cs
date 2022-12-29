// See https://aka.ms/new-console-template for more information

using Day13;


int CompareLines(string[] str)
{
    var a = new Parser(str[0]);
    var b = new Parser(str[1]);

    return a.RootNode().CompareTo(b.RootNode());
}


void Exercise1(string path)
{
    var res = File.ReadLines(path).Chunk(3).Select(lines => CompareLines(lines));

    int sum = 0;
    int i = 0;

    foreach (var v in res)
    {
        i++;
        if (v == -1)
            sum += i;
    }

    Console.WriteLine(sum);
}

Node CreateMarker(int v)
{
    var marker = Node.CreateList();
    var l = Node.CreateList();
    marker.Metadata = v;

    marker.Children.Add(l);
    l.Children.Add(Node.CreateValue(v));

    return marker;
}

void Exercise2(string path)
{
    var list = File.ReadLines(path)
        .Where(line => !string.IsNullOrEmpty(line))
        .Select(str => (new Parser(str)).RootNode())
        .ToList();

    list.Add(CreateMarker(2));
    list.Add(CreateMarker(6));

    list.Sort();

    int m1 = list.FindIndex(node => node.Metadata == 2);
    int m2 = list.FindIndex(node => node.Metadata == 6);

    int res = (m1 + 1) * (m2 + 1);
    Console.WriteLine(res);
}

string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);


