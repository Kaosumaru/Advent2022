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

#if true
                foreach (var valve in NotYetVisited)
                {
                    var d = CloserTo1(valve);

                    if (d >= -2)
                    {
                        var move = CreateMovement(this, valve, 0);
                        if (move != null)
                            yield return move;
                    }

                    if (d <= 2)
                    {
                        var move = CreateMovement(this, valve, 1);
                        if (move != null)
                            yield return move;
                    }

                }
#else
                foreach (var valve in NotYetVisited)
                {
                    for (int i = 0; i < Valve.Length; i++)
                    {
                        var move = CreateMovement(this, valve, i);
                        if (move != null)
                            yield return move;
                    }
                }
#endif
            }

            int CloserTo1(Valve a)
            {
                int distance1 = a._parent.GetDistance(Valve[0], a);
                int distance2 = a._parent.GetDistance(Valve[1], a);
                return distance1 - distance2;
            }

            public object GetGameState()
            {
                return (Valve[0], Valve[1]);
            }
        }

    }
}
