using Utils;

namespace Day22
{
    class CubeParser
    {
        public CubeParser(GenericGrid<int> grid, int width)
        {
            _grid = grid;
            _width = width;
        }

        public Cube GetCube()
        {
            if (_cube != null)
                return _cube;

            _cube = new Cube(_width);
            foreach (var p in FacePositions())
            {
                var subgrid = _grid.SubGrid(p, new(_width, _width));
                _faces.Add(subgrid);
            }

            return _cube;
        }

        IEnumerable<Vector2Int> FacePositions()
        {
            for (int y = 0; y < _grid.Height; y += _width)
                for (int x = 0; x < _grid.Width; x += _width)
                {
                    int v = _grid.GetValue(new(x, y));
                    if (v == 2)
                        continue;
                    yield return new(x, y);
                }
        }

        public void Display()
        {
            int i = 1;
            foreach (var face in _faces)
            {
                Console.WriteLine($"Face {i}");
                face.Display(v => v.ToString());
                Console.WriteLine($"");
                i++;
            }
        }

        Cube _cube;
        List<GenericGrid<int>> _faces = new();

        GenericGrid<int> _grid;
        int _width;
    }


}
