using Utils.Graph;
using Utils;
using StringExtensions;
using System.Text.RegularExpressions;

void ParseInput(string path, out int[] directions, out SimpleGraph<string, SimpleNode> graph)
{
    using StreamReader file = new(path);
    directions = file.ReadLine().ToArray().Select(x => x == 'L' ? 0 : 1).ToArray();


    file.ReadLine();

    graph = new();
    while (!file.EndOfStream)
    {
        var line = file.ReadLine();

        var (id, connections, _) = line.Split(" = ");

        string pattern = @"\((\w+), (\w+)\)";
        Regex rg = new(pattern);
        var mt = rg.Match(connections);

        var node = graph.GetNode(id);

        node.AddConnection(graph.GetNode(mt.Groups[1].Value));
        node.AddConnection(graph.GetNode(mt.Groups[2].Value));
    }

}

int StepsTo(int[] directions, SimpleNode node, string goal)
{
    int steps = 0;

    foreach(var direction in directions.RepeatForever())
    {
        steps++;
        node = node.GetConnectionList()[direction].Node as SimpleNode;
        if ((node.id() as string).EndsWith(goal))
            break;
    }

    return steps;
}

long StepsToGhost(int[] directions, SimpleGraph<string, SimpleNode> graph, string startPostfix, string postfix)
{
    return graph
        .GetNodes()
        .Where(pair => pair.Key.EndsWith(startPostfix))
        .Select(pair => StepsTo(directions, pair.Value, postfix))
        .Select(step => (long)step)
        .Lcm();
}

string path = "../../../data2.txt";
ParseInput(path, out var directions, out var graph);

// exercise1
var steps = StepsTo(directions, graph.GetNode("AAA"), "ZZZ");
Console.WriteLine(steps);

// exercise2
var steps2 = StepsToGhost(directions, graph, "A", "Z");
Console.WriteLine(steps2);