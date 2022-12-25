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
    /*
    var lines = File.ReadLines(path);

    foreach (var line in lines)
    {
        Console.WriteLine(line);
        Console.WriteLine(SNAFU.FromString(line));

        Console.WriteLine("");
    }


    */
    var snafus = File.ReadLines(path).Select(SNAFU.FromString);
    var snafu = new SNAFU();

    foreach (var s in snafus)
    {
        snafu = snafu + s;
    }

    Console.WriteLine(snafu);
}

// 20==--5=-4-5-3-5-14-4-13-101-40-4-3
string path = "../../../data2.txt";
Exercise1(path);


