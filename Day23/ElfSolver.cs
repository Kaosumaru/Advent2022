using Utils;

namespace Day23
{
    class ElfSolver
    {
        public ElfSolver(InfiniteGrid<Vector2Int, Elf> grid)
        {
            _grid = grid;
            _elves = _grid.GetGrid().Select(e => e.Value).ToList();
        }


        public void Turns(int amount)
        {
            for (int i = 0; i < amount; i++)
                Turn();
        }

        public bool Turn()
        {
            _proposedTargets = new();

            foreach (var elf in _elves)
                ProposeMove(elf);


            bool anyMoved = false;
            foreach (var elf in _elves)
                anyMoved = TryToMove(elf) || anyMoved;

            CurrentTurn++;

            return anyMoved;
        }

        public void TurnsUntilNotMoved()
        {
            while (Turn())
            {

            }
        }

        public int EmptySpaces()
        {
            var (min, max) = _elves.Select(e => e.Position).Bounds();
            var positions = Vector2IntExtensions.Between(min, max);
            return _grid.ValuesIn(positions).Where(e => e == null).Count();
        }

        void ProposeMove(Elf elf)
        {
            var p = elf.Position;
            elf.NextTarget = elf.Position;

            if (neighbors.All(d => _grid.GetValue(p + d) == null))
                return;

            foreach (var i in lookOrder())
            {
                if (proposedDir[i].Any(d => _grid.GetValue(p + d) != null))
                    continue;

                elf.NextTarget = p + directions[i];

                int v = _proposedTargets.GetValue(elf.NextTarget);
                _proposedTargets.SetValue(elf.NextTarget, v + 1);
                break;
            }
        }

        IEnumerable<int> lookOrder()
        {
            int m = CurrentTurn % 4;

            for (int i = m; i < 4; i++)
                yield return i;
            for (int i = 0; i < m; i++)
                yield return i;
        }

        bool TryToMove(Elf elf)
        {
            if (_proposedTargets.GetValue(elf.NextTarget) != 1)
                return false;

            if (elf.NextTarget == elf.Position)
                return false;

            _grid.RemoveValue(elf.Position);
            elf.Position = elf.NextTarget;
            _grid.SetValue(elf.Position, elf);
            return true;
        }

        public void DebugDisplay(Vector2Int start, Vector2Int end)
        {
            for (int y = start.y; y <= end.y; y++)
            {
                for (int x = start.x; x < end.x; x++)
                    Console.Write(_grid.GetValue(new Vector2Int(x, y)) == null ? '.' : '#');
                Console.WriteLine();
            }
        }

        protected static Vector2Int[] directions = new Vector2Int[] {
            new(0,-1), // N
            new(0,1), // S
            new(-1,0), // W
            new(1,0), // E
        };

        protected static Vector2Int[] neighbors = new Vector2Int[] {
            new(-1, -1),
            new(0, -1),
            new(1, -1),
            new(-1, 0),
            new(1, 0),
            new(-1, 1),
            new(0, 1),
            new(1, 1),
        };

        protected static Vector2Int[][] proposedDir = new Vector2Int[][] {
            new Vector2Int[]{ new(-1,-1), new(0,-1), new(1,-1), }, // N
            new Vector2Int[]{ new(-1,1), new(0,1), new(1,1), }, // S
            new Vector2Int[]{ new(-1,-1), new(-1,0), new(-1,1), }, // W
            new Vector2Int[]{ new(1,-1), new(1,0), new(1,1), }, // E
        };

        InfiniteGrid<Vector2Int, int> _proposedTargets;

        List<Elf> _elves;
        InfiniteGrid<Vector2Int, Elf> _grid;

        public int CurrentTurn { get; protected set; }
    }
}
