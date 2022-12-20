// See https://aka.ms/new-console-template for more information
using Day19;
using Utils;

Console.WriteLine("Hello, World!");

Blueprint bp = new Blueprint();
bp.Costs[Resources.ORE][Resources.ORE] = 4;

bp.Costs[Resources.CLAY][Resources.ORE] = 2;

bp.Costs[Resources.OBSIDIAN][Resources.ORE] = 3;
bp.Costs[Resources.OBSIDIAN][Resources.CLAY] = 14;

bp.Costs[Resources.GEODE][Resources.ORE] = 2;
bp.Costs[Resources.GEODE][Resources.OBSIDIAN] = 7;

var start = Move.CreateStartingMove(bp, 24);
MoveSolver<Move> solver = new();
solver.FindPathFrom(start);

var move = solver.BestMove;
Console.WriteLine(solver.BestMove.GetScore());