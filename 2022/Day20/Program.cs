// See https://aka.ms/new-console-template for more information


using Day20;


void Exercise(string path, long key = 1, long mixAmount = 1)
{
    var num = File.ReadLines(path).Select(line => long.Parse(line) * key);

    var numbers = new Numbers();
    foreach (var n in num)
        numbers.Add(n);

    numbers.Finish();

    for (int i = 0; i < mixAmount; i++)
        numbers.Mix();

    var res = numbers.Result();

    Console.WriteLine(res);
}



string path = "../../../data2.txt";
Exercise(path, 1, 1);
Exercise(path, 811589153, 10);
