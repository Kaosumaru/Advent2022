string path = "../../../data2.txt";

IEnumerable<int> SumOf(IEnumerable<int> l, int c)
{
    return Enumerable.Range(0, l.Count() - c + 1)
        .Select(i => l.Skip(i).Take(c).Sum());
}

int CountIncreasing(IEnumerable<int> l)
{
    return l
        .Zip(l.Skip(1))
        .Where(t => t.First < t.Second)
        .Count();
}

var lines = File.ReadLines(path).Select(l => int.Parse(l)).ToList();
Console.WriteLine(CountIncreasing(lines));

var normalizedLines = SumOf(lines, 3).ToList();
Console.WriteLine(CountIncreasing(normalizedLines));