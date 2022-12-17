

using Day17;
using Utils;

Vector2Long CharToDir(char c)
{
    if (c == '<')
        return Vector2Long.Left;
    return Vector2Long.Right;
}


string path = "../../../data.txt";
var movement = File.ReadAllText(path).Select(CharToDir).ToList();

var logic = new Logic(movement);
logic.CalculateTurns(2022);
Console.WriteLine(logic.Height);

// TODO detect cycle
// var logic2 = new Logic(movement);
// logic2.CalculateTurns(1000000000000);
// Console.WriteLine(logic.Height);