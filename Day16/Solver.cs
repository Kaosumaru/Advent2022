namespace Day16
{
    using FibonacciHeap;

    namespace Utils
    {
        internal class MoveNode
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

            public bool IsOtherMoveBestOrEqual(MoveNode node)
            {
                if (node == null)
                    return false;
                return node.Score >= Score && node.RemainingTime >= RemainingTime;
            }

            public bool IsThisMoveBetterScored(MoveNode node)
            {
                if (node == null)
                    return true;
                return node.Score < Score;
            }

            public long Score;
            public Valve Valve;

            public int RemainingTime = 30;
            protected HashSet<Valve> NotYetVisited = new();
            public List<string> Visited;

            public IEnumerable<MoveNode> GetConnections()
            {
                return NotYetVisited
                    .Select(valve => CreateMovement(this, valve))
                    .Where(move => move != null);
            }
        }



        internal class Solver
        {
            internal class ValveInfo
            {
                public MoveNode BestMove;
            }

            public void FindPathFrom(MoveNode node)
            {
                TryToSetNewScore(node);
                StartSearching();
            }


            void StartSearching()
            {
                while (!_priority.IsEmpty())
                {
                    var top = _priority.RemoveMin().Data;
                    UpdateNeighbors(top);
                }
            }

            public long GetScore(Valve node)
            {
                if (_bestScore.TryGetValue(node, out var wrapper))
                    return wrapper.BestMove.Score;
                return long.MinValue;
            }

            ValveInfo GetWrapper(Valve node)
            {
                if (_bestScore.TryGetValue(node, out var wrapper))
                    return wrapper;
                wrapper = new();
                //wrapper.Node = node;
                _bestScore[node] = wrapper;
                return wrapper;
            }

            void UpdateNeighbors(MoveNode node)
            {
                var connections = node.GetConnections();
                foreach (var connection in connections)
                {
                    TryToSetNewScore(connection);
                }
            }

            public ValveInfo BestScore()
            {
                return _bestScore.Select(p => p.Value).MaxBy(p => p.BestMove.Score);
            }

            void TryToSetNewScore(MoveNode node)
            {
                var wrapper = GetWrapper(node.Valve);
                if (node.IsOtherMoveBestOrEqual(wrapper.BestMove))
                    return;

                if (node.IsThisMoveBetterScored(wrapper.BestMove))
                {
                    wrapper.BestMove = node;
                }

                // insert into sorted list of nearest nodes
                // TODO we also could remove duplicate entries
                _priority.Insert(new FibonacciHeapNode<MoveNode, long>(node, node.Score));
            }

            Dictionary<Valve, ValveInfo> _bestScore = new();
            FibonacciHeap<MoveNode, long> _priority = new(0);
        }
    }
}
