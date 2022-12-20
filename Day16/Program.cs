// TODO
using Day16;
using Day16.Utils;
using System.Text.RegularExpressions;
using Utils;

void LineToValve(Valves valves, string line)
{
    // Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
    string pattern = @"Valve (\w*) has flow rate=([0-9]*); tunnels? leads? to valves? (.*)";
    Regex rg = new(pattern);

    var mt = rg.Match(line);

    var id = mt.Groups[1].Value;
    var flow = int.Parse(mt.Groups[2].Value);
    var connections = mt.Groups[3].Value.Split(", ");

    var v = valves.Get(id);
    v.Flow = flow;
    v.Connections = connections.Select(id => valves.Get(id)).ToList();
}

string path = "../../../data2.txt";
Valves valves = new();
foreach (var line in File.ReadLines(path))
    LineToValve(valves, line);

valves.Calculate();


MoveSolver<MoveNode> solver = new();
var start = MoveNode.CreateStartingPoint(valves.Get("AA"), 30);
solver.FindPathFrom(start);
var best = solver.BestScore();

Console.WriteLine(best.BestMove.Score);
