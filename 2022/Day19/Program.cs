// See https://aka.ms/new-console-template for more information
using Day19;
using System.Text.RegularExpressions;
using Utils;

Blueprint StringToBluePrint(string str)
{
    var robots = str.Split(". ");
    //var regex = 

    Regex[] rg = new Regex[]
    {
        new(@"(\d+) ore"),
        new(@"(\d+) clay"),
        new(@"(\d+) obsidian")
    };

    Blueprint bp = new Blueprint();

    int robotIndex = 0;
    foreach (var robot in robots)
    {
        int resIndex = 0;
        foreach (var regex in rg)
        {
            var mt = regex.Match(robot);
            if (!mt.Success)
            {
                resIndex++;
                continue;
            }

            bp.Costs[robotIndex][resIndex] = int.Parse(mt.Groups[1].Value);
            resIndex++;
        }

        robotIndex++;
    }
    return bp;
}


long ScoreForBluePrint(Blueprint bp, int time)
{
    var start = Move.CreateStartingMove(bp, time);
    MoveSolver<Move> solver = new();
    solver.FindPathFrom(start);

    return solver.BestMove.GetScore();
}

void Exercise1(string path)
{
    long totalScore = 0;
    long index = 1;
    foreach (var line in File.ReadLines(path))
    {
        Console.WriteLine($"Calculating for {index}");

        var bp = StringToBluePrint(line);
        long score = ScoreForBluePrint(bp, 24);
        totalScore += index * score;
        index++;
    }

    Console.WriteLine(totalScore);
}

void Exercise2(string path)
{
    long totalScore = 1;
    long index = 1;
    foreach (var line in File.ReadLines(path).Take(3))
    {
        Console.WriteLine($"Calculating for {index}");

        var bp = StringToBluePrint(line);
        long score = ScoreForBluePrint(bp, 32);
        totalScore *= score;
        index++;
    }

    Console.WriteLine(totalScore);
}

string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);
