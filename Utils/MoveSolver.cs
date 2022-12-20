using FibonacciHeap;

namespace Utils
{
    public interface IMove<T>
    {
        // used to compare with other Move, can represent field in game, or turn
        public object GetGameState();

        public bool IsOtherMoveBestOrEqual(T node);

        public bool IsThisMoveBetterScored(T node);

        public long GetScore();

        public IEnumerable<T> GetConnections();
    }


    public class MoveSolver<T> where T : IMove<T>
    {
        public class MoveInfo
        {
            public T BestMove;
        }

        public void FindPathFrom(T node)
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

        public long GetScore(object node)
        {
            if (_bestScore.TryGetValue(node, out var wrapper))
                return wrapper.BestMove.GetScore();
            return long.MinValue;
        }

        MoveInfo GetWrapper(object node)
        {
            if (_bestScore.TryGetValue(node, out var wrapper))
                return wrapper;
            wrapper = new();
            _bestScore[node] = wrapper;
            return wrapper;
        }

        void UpdateNeighbors(T node)
        {
            var connections = node.GetConnections();
            foreach (var connection in connections)
            {
                TryToSetNewScore(connection);
            }
        }

        public MoveInfo BestScore()
        {
            return _bestScore.Select(p => p.Value).MaxBy(p => p.BestMove.GetScore());
        }

        void TryToSetNewScore(T node)
        {
            var state = node.GetGameState();
            if (state != null)
            {
                var wrapper = GetWrapper(state);
                if (node.IsOtherMoveBestOrEqual(wrapper.BestMove))
                    return;

                if (node.IsThisMoveBetterScored(wrapper.BestMove))
                {
                    wrapper.BestMove = node;
                }
            }



            // insert into sorted list of nearest nodes
            // TODO we also could remove duplicate entries
            _priority.Insert(new(node, node.GetScore()));
        }

        Dictionary<object, MoveInfo> _bestScore = new();
        FibonacciHeap<T, long> _priority = new(0);
    }
}
