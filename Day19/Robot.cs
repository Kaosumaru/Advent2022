using Utils;

namespace Day19
{
    class Blueprint
    {
        public Resources[] Costs = new Resources[4];
    }


    internal class Move : IMove<Move>
    {
        static public Move CreateStartingMove(Blueprint bp, int time)
        {
            Move m = new();
            m.Blueprint = bp;
            m.Gain[0] = 1;
            m.TimeLeft = time;
            return m;
        }


        Move BuildRobotMove(int index)
        {
            var cost = Blueprint.Costs[index];
            var delta = cost - Collected;


            int waitTime = 0;
            for (int i = 0; i < Resources.Count; i++)
            {
                if (delta[i] <= 0)
                    continue;

                // we cannot build that, nothing is producing missing resource
                var g = Gain[i];
                if (g == 0)
                    return null;

                int wait = MathUtils.DivideUp(delta[i], g);

                if (wait > waitTime)
                    waitTime = wait;
            }

            // spend one minute to build robot
            waitTime++;

            // no time left to build!
            if (waitTime > TimeLeft)
                return null;

            var m = Clone();
            m.Parent = this;
            m.BuildRobot = index;

            m.Collected += m.Gain * waitTime - cost;
            m.Gain[index]++;
            m.TimeLeft -= waitTime;
            m.Turn++;

            //Console.WriteLine($"Built {index} robot at {m.TimeLeft}");

            // add all geodes collected by this robot to sum
            if (index == Resources.GEODE)
            {
                m._totalGeodes += m.TimeLeft;
            }


            return m;
        }
        Move Clone()
        {
            Move m = new();
            m.Blueprint = Blueprint;
            m.TimeLeft = TimeLeft;
            m.Gain = Gain;
            m.Collected = Collected;
            m._totalGeodes = _totalGeodes;
            m.Turn = Turn;
            return m;
        }

        public object GetGameState()
        {
            // todo optimize
            return TimeLeft;
        }

        public bool IsOtherMoveBestOrEqual(Move move)
        {
            if (move == null)
                return false;
            return move._totalGeodes > _totalGeodes;
        }

        public long GetScore()
        {
            return _totalGeodes;
        }

        public IEnumerable<Move> GetConnections()
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                var m = BuildRobotMove(i);
                if (m != null)
                    yield return m;
            }
        }

        public void DebugPrint()
        {
            List<Move> moves = new();

            Move current = this;
            while (current != null)
            {
                moves.Add(current);
                current = current.Parent;
            }

            moves.Reverse();
            foreach (var entry in moves)
            {
                Resources cost = new();
                if (entry.BuildRobot != -1)
                    cost = entry.Blueprint.Costs[entry.BuildRobot];

                Console.WriteLine($"{entry.Turn}");
                Console.WriteLine($"{entry.TimeLeft} -> {entry.BuildRobot}");
                Console.WriteLine($"C: {entry.Collected + cost}");
                Console.WriteLine($"G: {entry.Gain}");
                Console.WriteLine($"-: {cost}");
                Console.WriteLine($"");
                Console.WriteLine($"");
            }
        }

        public Blueprint Blueprint;
        public int TimeLeft;
        public Resources Gain;
        public Resources Collected;
        public long _totalGeodes = 0;
        public int Turn = 0;


        public Move Parent;
        public int BuildRobot = -1;
    }
}
