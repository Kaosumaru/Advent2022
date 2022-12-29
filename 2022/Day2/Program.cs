using Day2;

IEnumerable<MoveEntry> GetMoves1(string path)
{
    foreach (var line in File.ReadLines(path))
    {
        MoveEntry move = new MoveEntry();
        move.Opponent = (MoveEntry.MoveType)(line[0] - 'A');
        move.Me = (MoveEntry.MoveType)(line[2] - 'X');
        yield return move;
    }
}

IEnumerable<MoveEntry> GetMoves2(string path)
{
    foreach (var line in File.ReadLines(path))
    {
        MoveEntry move = new MoveEntry();
        move.Opponent = (MoveEntry.MoveType)(line[0] - 'A');

        var guide = line[2];
        if (guide == 'Z')
            move.Me = MoveEntry.WinningResponseFor(move.Opponent);
        else if (guide == 'Y')
            move.Me = MoveEntry.DrawResponseFor(move.Opponent);
        else
            move.Me = MoveEntry.LosingResponseFor(move.Opponent);

        yield return move;
    }
}

int ScoreMove(MoveEntry move)
{
    int[] moveScores = new int[] { 1, 2, 3 };
    int score = moveScores[(int)move.Me];

    if (move.IsADraw())
        score += 3;
    else if (move.DidIWin())
        score += 6;

    return score;
}

string path = "../../../data2.txt";
var totalPoints = GetMoves1(path).Select(ScoreMove).Sum();
Console.WriteLine(totalPoints);

var totalPoints2 = GetMoves2(path).Select(ScoreMove).Sum();
Console.WriteLine(totalPoints2);