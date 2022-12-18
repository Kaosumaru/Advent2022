

using Day17;
using Utils;

Vector2Long CharToDir(char c)
{
    if (c == '<')
        return Vector2Long.Left;
    return Vector2Long.Right;
}


#if false
string path = "../../../data.txt";
int prefixL = 25;
int loopL = 53;
int turns = 2022;
#else
string path = "../../../data2.txt";
int prefixL = 925;
int loopL = 2618;
int turns = 100000;

// answer not 1535756154727
#endif


// prefix 15, loop 35

var movement = File.ReadAllText(path).Select(CharToDir).ToList();

var logic = new Logic(movement);
logic.CalculateTurns(turns, prefixL, loopL);
Console.WriteLine(logic.Height);
logic.DebugInfo();
Console.WriteLine(logic.CalculateAtTurn(1000000000000));



// TODO detect cycle
// var logic2 = new Logic(movement);
// logic2.CalculateTurns(1000000000000);
// Console.WriteLine(logic.Height);