using Day3;
using Utils;

int Points1(Card card)
{
    int matches = card.matches;
    if (matches <= 1)
        return matches;
    return (int)Math.Pow(2, matches - 1);
}

void Exercise1(string path)
{
    CardSet set = new();
    var sum = File.ReadLines(path)
        .Select(line => Card.FromString(set, line))
        .Select(Points1)
        .Sum();

    Console.WriteLine(sum);
}

void Exercise2(string path)
{
    CardSet set = new();
    var sumOfCards = File.ReadLines(path)
        .Select(line => Card.FromString(set, line))
        .Count();

    foreach(var pair in set.cards)
    {
        sumOfCards += GraphUtils.IterateAllNodesRecursively(pair.Value).Count();
    }

    Console.WriteLine(sumOfCards);
}


string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);