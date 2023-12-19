using Day3;

void Exercise1(string path)
{
    var sum = File.ReadLines(path)
        .Select(Card.FromString)
        .Select(card => card.Points())
        .Sum();

    Console.WriteLine(sum);
}

string path = "../../../data.txt";
Exercise1(path);
