using Day2;

void Exercise1(string path)
{
    var max = new int[] { 12, 13, 14 };
    var sum = File.ReadLines(path)
        .Select(Game.FromString)
        .Where(game => game.IsValid(max))
        .Select(Game => Game.id)
        .Sum();

    Console.WriteLine(sum);
}

void Exercise2(string path)
{
    var sum = File.ReadLines(path)
        .Select(Game.FromString)
        .Select(Game => Game.Power())
        .Sum();

    Console.WriteLine(sum);
}

string path = "../../../data.txt";
Exercise1(path);
Exercise2(path);