using Utils;

namespace Day16
{
    namespace Utils
    {
        internal class MoveNode : IMove<MoveNode>
        {
            public static MoveNode CreateStartingPoint(Valve start, int time)
            {
                var node = new MoveNode();
                node.Valve = start;
                node.RemainingTime = time;
                node.NotYetVisited = start._parent.ImportantValves.ToHashSet();
                node.Score = 0;

                node.Visited = new();


                return node;
            }

            public static MoveNode CreateMovement(MoveNode start, Valve target)
            {
                var node = new MoveNode();
                node.Valve = target;
                node.RemainingTime = start.RemainingTime;
                node.NotYetVisited = new(start.NotYetVisited);
                node.NotYetVisited.Remove(target);

                node.Visited = new(start.Visited);
                node.Visited.Add(target.Id);

                node.Score = start.Score;

                int distance = target._parent.GetDistance(start.Valve, target);
                node.RemainingTime -= distance + 1;
                if (node.RemainingTime < 0)
                    return null;

                node.Score += target.Flow * node.RemainingTime;

                return node;
            }



            public long Score;
            public Valve Valve;

            public int RemainingTime = 30;
            protected HashSet<Valve> NotYetVisited = new();
            public List<string> Visited;

            public long GetScore()
            {
                return Score;
            }

            public bool IsOtherMoveBestOrEqual(MoveNode node)
            {
                if (node == null)
                    return false;
                return node.Score >= Score && node.RemainingTime >= RemainingTime;
            }

            public IEnumerable<MoveNode> GetConnections()
            {
                return NotYetVisited
                    .Select(valve => CreateMovement(this, valve))
                    .Where(move => move != null);
            }

            public object GetGameState()
            {
                return Valve;
            }
        }

    }
}
