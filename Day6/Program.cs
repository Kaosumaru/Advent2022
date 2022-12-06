// See https://aka.ms/new-console-template for more information


bool IsMarker(string buffer, int index, int count)
{
    return buffer
        .AsSpan(index - count + 1, count)
        .ToArray()
        .Distinct()
        .Count() == count;
}

int IndexOfMarker(string buffer, int count)
{
    for (int i = count - 1; i < buffer.Length; i++)
        if (IsMarker(buffer, i, count))
            return i;
    return -1;
}

string path = "../../../data2.txt";
var buffer = File.ReadAllText(path);

var marker = IndexOfMarker(buffer, 4);
Console.WriteLine(marker + 1);

var marker2 = IndexOfMarker(buffer, 14);
Console.WriteLine(marker2 + 1);