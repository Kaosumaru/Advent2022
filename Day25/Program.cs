/*
 * 2022

   base 5
   625 125 25 5 1
   3   1   0  4 2

1   =    1   1  - 2
*/

using Day25;

void Exercise1(string path)
{
    var snafus = File.ReadLines(path).Select(SNAFU.FromString);
    var snafu = new SNAFU();

    foreach (var s in snafus)
    {
        snafu = snafu + s;
    }

    Console.WriteLine(snafu);
}

string path = "../../../data2.txt";
Exercise1(path);


