
CrateStacks ParseStacks(StreamReader file)
{
    int stackCount = 0;
    CrateStacks? stacks = null;

    while (true)
    {
        var line = file.ReadLine();
        if (line == null)
            return null;

        if (stacks == null)
        {
            stackCount = (line.Length + 1) / 4;
            stacks = new(stackCount);
        }


        for (int i = 0; i < stackCount; i++)
        {
            int index = 1 + i * 4;
            var entry = line[index];
            if (entry == '1')
            {
                stacks.Reverse();
                return stacks;
            }


            if (entry == ' ')
                continue;

            stacks.Stacks[i].Boxes.Push(entry);
        }
    }
}

IEnumerable<Tuple<int, int, int>> ParseCommands(StreamReader file, CrateStacks stacks)
{
    while (true)
    {
        var line = file.ReadLine();
        if (line == null)
            yield break;

        var split = line.Split(' ');
        int amount = int.Parse(split[1]);
        int from = int.Parse(split[3]) - 1;
        int to = int.Parse(split[5]) - 1;

        yield return Tuple.Create(amount, from, to);
    }
}

void DisplayTop(CrateStacks stacks)
{
    foreach (var stack in stacks.Stacks)
    {
        Console.Write(stack.Boxes.Peek());
    }
}

string path = "../../../data2.txt";

using (StreamReader file = new(path))
{
    var stacks = ParseStacks(file);
    file.ReadLine();
    foreach (var command in ParseCommands(file, stacks))
    {
        stacks.MoveBoxes(command.Item1, command.Item2, command.Item3);
    }

    DisplayTop(stacks);
}

Console.WriteLine();

using (StreamReader file = new(path))
{
    var stacks = ParseStacks(file);
    file.ReadLine();
    foreach (var command in ParseCommands(file, stacks))
    {
        stacks.MoveBoxes2(command.Item1, command.Item2, command.Item3);
    }

    DisplayTop(stacks);
}