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

long GetScore<T>(T start) where T : IMove<T>
{
    MoveSolver<T> solver = new();
    solver.FindPathFrom(start);
    return solver.BestMove.GetScore();
}

long GetScore2(Valves valves, string id, int time)
{
    // that's not an exactly right answer, since it assumes
    // - agent is moving using best path
    // - the agent B is moving using best path
    // but agents could achieve better results by cooperation
    // potential solution - save best result in time for node, calculate which agent got there faster

    var startValve = valves.Get(id);
    var start = MoveNode.CreateStartingPoint(startValve, time);

    MoveSolver<MoveNode> solver = new();
    solver.FindPathFrom(start);
    var move = solver.BestMove;

    move.RemainingTime = time;
    move.Valve = startValve;
    solver = new();
    solver.FindPathFrom(move);
    return solver.BestMove.GetScore();
}

string path = "../../../data2.txt";
Valves valves = new();
foreach (var line in File.ReadLines(path))
    LineToValve(valves, line);

valves.Calculate();

var start = MoveNode.CreateStartingPoint(valves.Get("AA"), 30);
Console.WriteLine(GetScore(start));
Console.WriteLine(GetScore2(valves, "AA", 26));