using Day23;
using Utils;

InfiniteGrid<Vector2Int, Elf> CreateGridFromFile(string path)
{
    var lines = File.ReadAllLines(path);
    var grid = new InfiniteGrid<Vector2Int, Elf>();

    for (int y = 0; y < lines.Length; y++)
        for (int x = 0; x < lines[0].Length; x++)
        {
            var v = lines[y][x];
            if (v == '#')
            {
                Vector2Int p = new(x, y);
                var elf = new Elf();
                elf.Position = p;

                grid.SetValue(p, elf);
            }

        }

    return grid;
}

void Exercise1(string path)
{
    var grid = CreateGridFromFile(path);
    var solver = new ElfSolver(grid);
    solver.Turns(10);
    Console.WriteLine(solver.EmptySpaces());
}

void Exercise2(string path)
{
    var grid = CreateGridFromFile(path);
    var solver = new ElfSolver(grid);
    solver.TurnsUntilNotMoved();
    Console.WriteLine(solver.CurrentTurn);
}


string path = "../../../data2.txt";
Exercise1(path);

// 955 too low
Exercise2(path);