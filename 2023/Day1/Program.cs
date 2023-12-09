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

string path = "../../../data.txt";
Exercise1(path);
