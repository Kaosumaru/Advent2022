using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Day15
{
    internal class Sensor
    {
        public Sensor()
        {

        }

        public Sensor(Vector2Int pos, int width)
        {
            Position = pos;
            Width = width;
        }

        public bool Contains(Vector2Int pos)
        {
            int d = Vector2Int.ManhattanDistance(Position, pos);
            return d <= Width;
        }

        public Vector2Int Position;
        public int Width;
    }
}
