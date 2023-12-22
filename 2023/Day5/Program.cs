// See https://aka.ms/new-console-template for more information
using Day5;
using System.Numerics;

Transformer CreateTransformer(string path, out List<long> input)
{
    using StreamReader file = new(path);
    input = file.ReadLine()?.Split(" ").Skip(1).Select(text => long.Parse(text)).ToList();

    Transformer transformer = new();
    file.ReadLine();

    while(!file.EndOfStream)
    {
        var name = file.ReadLine();
        TransformerLayer layer = new(name);
        transformer.AddLayer(layer);

        while (true)
        {
            var line = file.ReadLine();
            if (line == null || line == "") break;
            var numbers = line.Split(" ").Select(text => long.Parse(text)).ToList();
            layer.AddRange(numbers[0], numbers[1], numbers[2]);
        }


    }

    return transformer;
}


void Exercise1(Transformer transformer, List<long> input)
{
    var lowest = input.Select(number => transformer.Transform(number)).Min();
    Console.WriteLine(lowest);
}

string path = "../../../data2.txt";
var transformer = CreateTransformer(path, out var input);

Exercise1(transformer, input);
