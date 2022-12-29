using System.Globalization;

namespace Day19
{
    struct Resources
    {
        public const int Count = 4;
        public const int ORE = 0;
        public const int CLAY = 1;
        public const int OBSIDIAN = 2;
        public const int GEODE = 3;

        int _ore;
        int _clay;
        int _obsidian;
        int _geode;

        public int this[int index]
        {

            get
            {
                switch (index)
                {
                    case 0: return _ore;
                    case 1: return _clay;
                    case 2: return _obsidian;
                    case 3: return _geode;
                    default:
                        throw new IndexOutOfRangeException(string.Format("Invalid Resources: {0}!", index));
                }
            }


            set
            {
                switch (index)
                {
                    case 0: _ore = value; break;
                    case 1: _clay = value; break;
                    case 2: _obsidian = value; break;
                    case 3: _geode = value; break;
                    default:
                        throw new IndexOutOfRangeException(string.Format("Invalid Resources: {0}!", index));
                }
            }
        }

        public static Resources operator +(Resources a, Resources b)
        {
            return new Resources(a._ore + b._ore, a._clay + b._clay, a._obsidian + b._obsidian, a._geode + b._geode);
        }


        public static Resources operator -(Resources a, Resources b)
        {
            return new Resources(a._ore - b._ore, a._clay - b._clay, a._obsidian - b._obsidian, a._geode - b._geode);
        }

        public static Resources operator *(Resources a, int b)
        {
            return new Resources(a._ore * b, a._clay * b, a._obsidian * b, a._geode * b);
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return string.Format("({0}, {1}, {2}, {3})",
                _ore.ToString(format, formatProvider),
                _clay.ToString(format, formatProvider),
                _obsidian.ToString(format, formatProvider),
                _geode.ToString(format, formatProvider)
                );
        }


        public Resources(int ore, int clay, int obsidian, int geode)
        {
            _ore = ore;
            _clay = clay;
            _obsidian = obsidian;
            _geode = geode;
        }
    }


}
