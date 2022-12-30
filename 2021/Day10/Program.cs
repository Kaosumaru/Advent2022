using Day10;



string path = "../../../data2.txt";
var totalScore = File.ReadLines(path).Select(Scoring.ScoreParse).Sum();
Console.WriteLine(totalScore);

var totalScores = File.ReadLines(path)
    .Select(Parser.Create)
    .Where(p => p != null)
    .Select(Scoring.ScoreAutocomplete)
    .OrderBy(x => x)
    .ToList();

// not 194061416
Console.WriteLine(totalScores[totalScores.Count / 2]);