
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Day3
{

    internal class Card
    {
        public Card(List<int> winningNumbers, List<int> numbers)
        {
            this.winningNumbers = winningNumbers;
            this.numbers = numbers;

            winningNumbersSet = new HashSet<int>(winningNumbers);
        }

        public static Card FromString(string line)
        {
            string pattern = @"Card\s+(\d*): (.*) \| (.*)";
            Regex rg = new(pattern);
            var mt = rg.Match(line);

            Card card = new(TextToList(mt.Groups[2].Value), TextToList(mt.Groups[3].Value));
            return card;
        }

        public int Points()
        {
            int matches = numbers.Where(number => winningNumbersSet.Contains(number)).Count();
            if (matches <= 1)
                return matches;
            return (int)Math.Pow(2, matches - 1);
        }

        static List<int> TextToList(string text)
        {
            return text.Split(" ")
                .Where(entry => entry.Length > 0)
                .Select(entry => int.Parse(entry))
                .ToList();
        }

        List<int> winningNumbers;
        List<int> numbers;

        HashSet<int> winningNumbersSet;
    }
}
