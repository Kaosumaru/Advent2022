using Utils;

namespace Day16
{
    internal class Valves
    {
        public Valves()
        {

        }

        public int ScorePath(IEnumerable<string> path)
        {
            int score = 0;
            int remainingTime = 30;

            Valve current = Get("AA");

            foreach (var id in path)
            {
                Valve next = Get(id);
                var distance = _paths[current.Id][id];
                remainingTime -= distance + 1;
                if (remainingTime < 0)
                    break;
                score += next.Flow * remainingTime;
                current = next;
            }

            return score;
        }

        public int GetDistance(Valve a, Valve b)
        {
            return _paths[a.Id][b.Id];
        }

        public Valve Get(string id)
        {
            if (_valves.TryGetValue(id, out var valve))
                return valve;
            valve = new Valve(id, this);
            _valves[id] = valve;
            return valve;
        }

        public void Calculate()
        {
            ImportantValves = _valves
                .Select(p => p.Value)
                .Where(v => v.Flow > 0)
                .ToList();

            GetDistance(Get("AA"));
            foreach (var v in ImportantValves)
                GetDistance(v);
        }


        void GetDistance(Valve a)
        {
            DjikstraPath path = new();
            path.FindPathTo(a);

            foreach (var b in ImportantValves)
                if (a != b)
                    AddPath(a.Id, b.Id, (int)path.GetDistance(b));
        }

        void AddPath(string a, string b, int v)
        {
            if (!_paths.TryGetValue(a, out var pathA))
            {
                pathA = new();
                _paths[a] = pathA;
            }

            pathA[b] = v;
        }

        public Dictionary<string, Dictionary<string, int>> _paths = new();

        public List<Valve> ImportantValves;
        public Dictionary<string, Valve> _valves = new();
    }
}
