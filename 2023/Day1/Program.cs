using Day1;

int LineToNumber(string line)
{
    var numbers = line.Where(c => c >= '0' && c <= '9')
        .Select(c => c - '0');
    return numbers.First() * 10 + numbers.Last();
}

void Exercise1(string path)
{
    var sum = File.ReadLines(path)
        .Select(LineToNumber)
        .Sum();

    Console.WriteLine(sum);
}

string FirstFoundString(IEnumerable<string> searchFor, string text)
{
    return searchFor.Select(entry => (entry, text.IndexOf(entry)))
        .Where(pair => pair.Item2 >= 0)
        .MinBy(pair => pair.Item2).entry; ;
}

string LastFoundString(IEnumerable<string> searchFor, string text)
{
    return searchFor.Select(entry => (entry, text.LastIndexOf(entry)))
        .Where(pair => pair.Item2 >= 0)
        .MaxBy(pair => pair.Item2).entry;
}

int LineToNumber2(string line)
{
    var firstString = FirstFoundString(Numbers.Entries.Keys, line);
    var lastString = LastFoundString(Numbers.Entries.Keys, line);

    int firstNumber = Numbers.Entries[firstString];
    int lastNumber = Numbers.Entries[lastString];

    return firstNumber * 10 + lastNumber;
}

void Exercise2(string path)
{
    var sum = File.ReadLines(path)
    .Select(LineToNumber2)
    .Sum();

    Console.WriteLine(sum);
}

string path = "../../../data.txt";
Exercise1(path);
Exercise2(path);