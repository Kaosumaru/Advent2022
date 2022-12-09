// See https://aka.ms/new-console-template for more information

Range StringToToRange(string str)
{
    var e = str.Split('-');
    return new Range
    {
        Start = int.Parse(e[0]),
        End = int.Parse(e[1])
    };
}

(Range, Range) StringToToRanges(string line)
{
    var e = line.Split(',');
    return (StringToToRange(e[0]), StringToToRange(e[1]));
}

bool IsOneRangeContainingOther((Range, Range) ranges)
{
    return ranges.Item1.Contains(ranges.Item2) || ranges.Item2.Contains(ranges.Item1);
}

string path = "../../../data2.txt";

var count = File.ReadLines(path)
    .Select(StringToToRanges)
    .Where(IsOneRangeContainingOther)
    .Count();

Console.WriteLine(count);

var overlaps = File.ReadLines(path)
    .Select(StringToToRanges)
    .Where(pair => pair.Item1.Overlaps(pair.Item2))
    .Count();

Console.WriteLine(overlaps);