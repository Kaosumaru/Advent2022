public class CrateStack
{
    public Stack<char> Boxes = new();

    public void Reverse()
    {
        Boxes = new(Boxes.ToArray());
    }
}

public class CrateStacks
{
    public List<CrateStack> Stacks;

    public CrateStacks(int count)
    {
        Stacks = Enumerable.Range(0, count)
            .Select(_ => new CrateStack())
            .ToList();
    }

    public void MoveBoxes(int amount, int from, int to)
    {
        var stackFrom = Stacks[from];
        var stackTo = Stacks[to];

        for (int i = 0; i < amount; i++)
        {
            var box = stackFrom.Boxes.Pop();
            stackTo.Boxes.Push(box);
        }
    }

    public void MoveBoxes2(int amount, int from, int to)
    {
        var stackFrom = Stacks[from];
        var stackTo = Stacks[to];

        List<char> boxes = new();

        for (int i = 0; i < amount; i++)
        {
            var box = stackFrom.Boxes.Pop();
            boxes.Add(box);
        }
        boxes.Reverse();

        foreach (var box in boxes)
        {
            stackTo.Boxes.Push(box);
        }
    }

    public void Reverse()
    {
        foreach (var stack in Stacks)
            stack.Reverse();
    }
}