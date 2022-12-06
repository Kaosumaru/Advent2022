// See https://aka.ms/new-console-template for more information

IEnumerable<string> GetRucksacks(string path)
{
    return File.ReadLines(path);
}


const int MaxPriority = 52;
int PriorityFor(char c)
{
    if (c >= 'a')
        return c - 'a' + 1;
    return c - 'A' + 27;
}

void AssignBitInTable(string rucksack, int from, int to, int bit, int[] rucksackContains)
{
    for (int i = from; i < to; i++)
    {
        int item = PriorityFor(rucksack[i]);
        rucksackContains[item - 1] |= bit;
    }
}

int FindCommonPriorityInRucksackCompartments(string rucksack)
{
    int compartmentSize = rucksack.Length / 2;
    int[] rucksackContains = new int[MaxPriority];

    AssignBitInTable(rucksack, 0, compartmentSize, 1, rucksackContains);
    AssignBitInTable(rucksack, compartmentSize, compartmentSize * 2, 2, rucksackContains);

    return Array.IndexOf(rucksackContains, 3) + 1;
}

int FindCommonPriorityInRucksacks(string[] rucksacks)
{
    int[] rucksackContains = new int[MaxPriority];
    AssignBitInTable(rucksacks[0], 0, rucksacks[0].Length, 1, rucksackContains);
    AssignBitInTable(rucksacks[1], 0, rucksacks[1].Length, 2, rucksackContains);
    AssignBitInTable(rucksacks[2], 0, rucksacks[2].Length, 4, rucksackContains);

    return Array.IndexOf(rucksackContains, 7) + 1;
}

string path = "../../../data2.txt";

var prioritySum = GetRucksacks(path).Select(FindCommonPriorityInRucksackCompartments).Sum();
Console.WriteLine(prioritySum);

var badgeSum = GetRucksacks(path).Chunk(3).Select(FindCommonPriorityInRucksacks).Sum();
Console.WriteLine(badgeSum);
