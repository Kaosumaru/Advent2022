// See https://aka.ms/new-console-template for more information


using Day20;

string path = "../../../data2.txt";
var num = File.ReadLines(path).Select(line => int.Parse(line));

var numbers = new Numbers();
foreach (var n in num)
    numbers.Add(n);

numbers.Finish();
numbers.MoveAllElements();

var res = numbers.Get(1000) + numbers.Get(2000) + numbers.Get(3000);

Console.WriteLine(res);
