using Day2;

string path = "../../../data2.txt";

var lines = File.ReadAllLines(path);

Submarine sub = new();
foreach (var line in lines)
    sub.ParseLine(line);
Console.WriteLine(sub.Position.x * sub.Position.y);

Submarine2 sub2 = new();
foreach (var line in lines)
    sub2.ParseLine(line);
Console.WriteLine(sub2.Position.x * sub2.Position.y);

