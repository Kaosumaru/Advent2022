// TODO
using Day16;
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

string path = "../../../data.txt";
Valves valves = new();
foreach (var line in File.ReadLines(path))
    LineToValve(valves, line);

valves.Calculate();

var possiblePath = valves.ImportantValves.Select(v => v.Id).ToList();
var maxScore = possiblePath
    .GetPermutations()
    .Select(path => valves.ScorePath(path))
    .Max();

Console.WriteLine(maxScore);


//List<string> p = new List<string>() { "DD", "BB", "JJ", "HH", "EE", "CC" };
//var score = valves.ScorePath(p);
//Console.WriteLine(score);
