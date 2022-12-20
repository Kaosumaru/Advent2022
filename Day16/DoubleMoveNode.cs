using Utils;

namespace Day16
{
    namespace Utils
    {
        internal class DoubleMoveNode : IMove<DoubleMoveNode>
        {
            public static DoubleMoveNode CreateStartingPoint(Valve start, int time)
            {
                var node = new DoubleMoveNode();

                for (int i = 0; i < node.Valve.Length; i++)
                    node.Valve[i] = start;

                for (int i = 0; i < node.RemainingTime.Length; i++)
                    node.RemainingTime[i] = time;

                node.NotYetVisited = start._parent.ImportantValves.ToHashSet();
                node.Score = 0;

                node.Visited = new();


                return node;
            }

            public static DoubleMoveNode CreateMovement(DoubleMoveNode start, Valve target, int actor)
            {
                var node = new DoubleMoveNode();

                node.Valve = start.Valve.ToArray();
                node.RemainingTime = start.RemainingTime.ToArray();
                node.Valve[actor] = target;


                int distance = target._parent.GetDistance(start.Valve[actor], target);
                node.RemainingTime[actor] -= distance + 1;
                if (node.RemainingTime[actor] < 0)
                    return null;

                node.NotYetVisited = new(start.NotYetVisited);
                node.NotYetVisited.Remove(target);

                node.Visited = new(start.Visited);
                node.Visited.Add(target.Id);

                node.Score = start.Score;
                node.Score += target.Flow * node.RemainingTime[actor];

                return node;
            }



            public long Score;
            public Valve[] Valve = new Valve[2];

            public int[] RemainingTime = new int[2];
            protected HashSet<Valve> NotYetVisited = new();
            public List<string> Visited;

            public long GetScore()
            {
                return Score;
            }

            public bool IsOtherMoveBestOrEqual(DoubleMoveNode node)
            {
                if (node == null)
                    return false;
                for (int i = 0; i < RemainingTime.Length; i++)
                    if (RemainingTime[i] >= node.RemainingTime[i])
                        return false;

                return node.Score >= Score;
            }

            public IEnumerable<DoubleMoveNode> GetConnections()
            {
                foreach (var valve in NotYetVisited)
                {
                    for (int i = 0; i < Valve.Length; i++)
                    {
                        var move = CreateMovement(this, valve, i);
                        if (move != null)
                            yield return move;
                    }
                }
            }

            public object GetGameState()
            {
                return (Valve[0], Valve[1]);
            }
        }

    }
}
