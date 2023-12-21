
using Utils;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections;

namespace Day3
{

    internal class CardSet
    {
        public CardSet() 
        { 
        }

        public void Add(int id, Card card) 
        {
            cards[id] = card;
        }

        public Card Get(int id)
        {
            return cards.GetValueOrDefault(id);
        }

        public Dictionary<int, Card> cards = new();
    }

    internal class Card : GraphNode
    {
        public Card(CardSet set, int id, List<int> winningNumbers, List<int> numbers)
        {
            this.id = id;
            set.Add(id, this);
            cardSet = set;

            HashSet<int> winningNumbersSet = new HashSet<int>(winningNumbers);
            matches = numbers.Where(number => winningNumbersSet.Contains(number)).Count();
        }

        public static Card FromString(CardSet set, string line)
        {
            string pattern = @"Card\s+(\d*): (.*) \| (.*)";
            Regex rg = new(pattern);
            var mt = rg.Match(line);

            int id = int.Parse(mt.Groups[1].Value);

            Card card = new(set, id, TextToList(mt.Groups[2].Value), TextToList(mt.Groups[3].Value));
            return card;
        }


        static List<int> TextToList(string text)
        {
            return text.Split(" ")
                .Where(entry => entry.Length > 0)
                .Select(entry => int.Parse(entry))
                .ToList();
        }

        public IEnumerable<GraphNode.ConnectionInfo> GetConnections()
        {
            return Enumerable
                .Range(id + 1, matches)
                .Select(number => cardSet.Get(number))
                .Where(card => card != null)
                .Select(card => new GraphNode.ConnectionInfo()
            {
                Node = card,
                Distance = 0,
            });
        }

        CardSet cardSet;
        public int id;
        public int matches;
    }
}
