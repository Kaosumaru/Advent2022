using Utils;

namespace Day22
{
    public struct FacePortal
    {
        public int Target;
        public int Direction;
        public bool Reversed;

        public FacePortal(int t, int d, bool r)
        {
            Target = t;
            Direction = d;
            Reversed = r;
        }
    }

    internal struct FaceData
    {
        public FacePortal[] Data;
    }


    internal class FaceInfo
    {
        public FaceInfo(int size, int width, GenericGrid<int> grid)
        {
            _size = size;
            _width = width;
            _grid = grid;
        }

        public (Vector2Int, int) PositionOnFace(Vector2Int oldPosition, int oldFace, int direction)
        {
            var data = _data[oldFace].Data[direction];

            var positionInOldFace = PositionInFace(oldPosition);
            int val = GetImportantValue(positionInOldFace, direction);

            Vector2Int newPos = StartOfFace(data.Target);

            if (data.Reversed)
                val = _size - 1 - val;

            switch (data.Direction)
            {
                case 0:
                    newPos.x += _size - 1;
                    newPos.y += val;
                    break;
                case 1:
                    newPos.y += _size - 1;
                    newPos.x += val;
                    break;
                case 2:
                    newPos.y += val;
                    break;
                case 3:
                    newPos.x += val;
                    break;
            }


            return (newPos, OppositeDirectionFor(data.Direction));
        }

        int OppositeDirectionFor(int d)
        {
            switch (d)
            {
                case 0:
                    return 2;
                case 1:
                    return 3;
                case 2:
                    return 0;
                case 3:
                    return 1;
            }
            throw new NotImplementedException();
        }

        int GetImportantValue(Vector2Int pos, int direction)
        {
            if (direction == 0 || direction == 2)
                return pos.y;
            return pos.x;
        }

        Vector2Int PositionInFace(Vector2Int pos)
        {
            pos.x %= _size;
            pos.y %= _size;
            return pos;
        }

        Vector2Int PositionInFaceToGlobal(Vector2Int pos, int face)
        {
            return StartOfFace(face) + pos;
        }

        Vector2Int StartOfFace(int face)
        {
            int x = face % _width;
            int y = face / _width;

            return new(x * _size, y * _size);
        }

        public int FaceOf(Vector2Int pos)
        {
            if (_grid.IsOutside(pos))
                return -1;

            int x = pos.x / _size;
            int y = pos.y / _size;

            return _width * y + x;
        }

        GenericGrid<int> _grid;
        int _width;
        int _size;
        static FaceData[] _data = CreateData();

        static public FaceData[] CreateData()
        {
            return new FaceData[]
            {
                // row 1
                // 0
                new FaceData(),
                // 1
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(2, 2, false),
                        new(4, 3, false),
                        new(6, 2, true),
                        new(9, 2, false),
                    },
                },
                // 2
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(7, 0, true),
                        new(4, 0, false),
                        new(1, 0, false),
                        new(9, 1, false),
                    },
                },

                // row 2
                // 3
                new FaceData(),
                // 4
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(2, 1, false),
                        new(7, 3, false),
                        new(6, 3, false),
                        new(1, 1, false),
                    },
                },
                // 5
                new FaceData(),

                // row 3
                // 6
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(7, 2, false),
                        new(9, 3, false),
                        new(1, 2, true),
                        new(4, 2, false),
                    },
                },
                // 7
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(2, 0, true),
                        new(9, 0, false),
                        new(6, 0, false),
                        new(4, 1, false),
                    },
                },
                // 8
                new FaceData(),

                // row 4
                // 9
                new FaceData()
                {
                    Data = new FacePortal[] {
                        new(7, 1, false),
                        new(2, 3, false),
                        new(1, 3, false),
                        new(6, 1, false),
                    },
                },
                // 10
                new FaceData(),
                // 11
                new FaceData(),

            };
        }
    }
}
