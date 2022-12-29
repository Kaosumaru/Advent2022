// See https://aka.ms/new-console-template for more information
using Day24;

void Exercise1(string path)
{
    BlizzardGrid3D grid = BlizzardGrid3D.CreateGridFromFile(path);
    Console.WriteLine(grid.FindShortestPathToEnd());
}

void Exercise2(string path)
{
    BlizzardGrid3D grid = BlizzardGrid3D.CreateGridFromFile(path);

    int t1 = grid.FindShortestPathToEnd();
    int t2 = grid.FindShortestPathToStart(t1);
    int t3 = grid.FindShortestPathToEnd(t1 + t2);

    Console.WriteLine(t1 + t2 + t3);
}

string path = "../../../data2.txt";
Exercise1(path);
Exercise2(path);



