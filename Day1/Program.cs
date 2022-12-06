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
    var sortedElfCalories = GetEveryElfCalories(file)
        .Select(elf => elf.Sum())
        .OrderByDescending(calories => calories).ToArray();

    // part 1
    var maxCalories = sortedElfCalories.First();
    Console.WriteLine(maxCalories);

    // part 2
    var top3 = sortedElfCalories.Take(3).Sum();
    Console.WriteLine(top3);

}