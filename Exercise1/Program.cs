IEnumerable<int> GetElfCalories(StreamReader input)
{
    while (true)
    {
        var line = input.ReadLine();
        if (string.IsNullOrEmpty(line))
            yield break;
        yield return int.Parse(line);
    }
}

IEnumerable<IEnumerable<int>> GetEveryElfCalories(StreamReader input)
{
    while (true)
    {
        if (input.EndOfStream)
            yield break;
        yield return GetElfCalories(input);
    }
}

string path = "../../../data2.txt";

using (StreamReader file = new(path))
{
    var maxElfCalories = GetEveryElfCalories(file)
        .Select(elf => elf.Sum())
        .Max();
    Console.WriteLine(maxElfCalories);
}