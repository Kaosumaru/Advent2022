using FibonacciHeap;

namespace Utils
{
    public interface IMove<T>
    {
        // used to compare with other Move, can represent field in game, or turn
        public object GetGameState();

        public bool IsOtherMoveBestOrEqual(T node);

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

        void TryToSetNewScore(T move)
        {
            var state = move.GetGameState();
            if (state != null)
            {
                var wrapper = GetWrapper(state);
                if (move.IsOtherMoveBestOrEqual(wrapper.BestMove))
                    return;

                if (IsThisMoveBetterScored(move, wrapper.BestMove))
                {
                    wrapper.BestMove = move;
                }
            }

            if (IsThisMoveBetterScored(move, BestMove))
            {
                BestMove = move;
            }


            // insert into sorted list of nearest nodes
            // TODO we also could remove duplicate entries
            _priority.Insert(new(move, -move.GetScore()));
        }

        public bool IsThisMoveBetterScored(T n, T old)
        {
            if (old == null)
                return true;
            return n.GetScore() > old.GetScore();
        }

        Dictionary<object, MoveInfo> _bestScore = new();
        FibonacciHeap<T, long> _priority = new(0);
        public T BestMove { get; protected set; }
    }
}
