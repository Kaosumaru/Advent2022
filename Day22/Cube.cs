using Utils;

namespace Day22
{
    internal class Cube
    {
        public Cube(int size)
        {
            _size = size;
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

        int _size;
        List<GenericGrid<int>> _faces;
    }
}
