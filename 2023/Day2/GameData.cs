
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Day2
{
    internal class CubeSet
    {
        internal enum CubeType
        {
            Red, Green, Blue
        }

        public static CubeSet FromString(string str)
        {
            CubeSet set = new();
            var entries = str.Split(", ");
            foreach (var entry in entries)
            {
                var fields = entry.Split(' ');
                int number = int.Parse(fields[0]);
                int index = NameToIndex(fields[1]);
                set.Cubes[index] = number;
            }
            return set;
        }

        static public int NameToIndex(string name) =>
           name switch
           {
               "red" => 0,
               "green" => 1,
               "blue" => 2,
               _ => throw new ArgumentException("Invalid enum value", nameof(name)),
           };

        public int[] Cubes = new int[3];

        internal bool IsValid(int[] max)
        {
            return Cubes.Zip(max, (cube, max) => cube <= max)
                .All(x => x);
        }
    }

    internal class Game
    {
        public static Game FromString(string line)
        {
            string pattern = @"Game (\d*): (.*)";
            Regex rg = new(pattern);
            var mt = rg.Match(line);

            Game game = new();
            game.id = int.Parse(mt.Groups[1].Value);

            string roundsString = mt.Groups[2].Value;
            game.Rounds = roundsString.Split("; ")
                .Select(CubeSet.FromString)
                .ToList();

            return game;
        }

        internal bool IsValid(int[] max)
        {
            return Rounds.All(round => round.IsValid(max));
        }

        internal int Max(int index)
        {
            return Rounds
                .Select(round => round.Cubes[index])
                .Max();
        }

        internal int Power()
        {
            return Max(0) * Max(1) * Max(2);
        }

        public int id = 0;
        List<CubeSet> Rounds = new();
    }
}
