using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Transformer
    {
        public void AddLayer(TransformerLayer layer)
        {
            layers.Add(layer);
        }

        public long Transform(long input)
        {
            foreach(var layer in layers)
            {
                input = layer.Transform(input);
            }
            return input;
        }


        List<TransformerLayer> layers = new();
    }

    internal class TransformerLayer
    {
        public TransformerLayer(string name)
        {
            this.name = name;
        }

        public void AddRange(long destination, long source, long length)
        {
            ranges.Add(new Range
            {
                Source = source,
                Destination = destination,
                Length = length
            });
        }

        public long Transform(long input)
        {
            foreach (Range range in ranges)
            {
                var output = range.TryToMatch(input);
                if (output.HasValue)
                    return output.Value;
            }
            return input;
        }

        struct Range
        {
            public long Source;
            public long Destination;
            public long Length;

            public long? TryToMatch(long input)
            {
                long delta = input - Source;
                if (delta < 0)
                    return null;
                if (delta > Length - 1)
                    return null;
                return Destination + delta;
            }
        }

        List<Range> ranges = new();
        string name;
    }
}
