using Day10;

string path = "../../../data2.txt";

int sum = 0;
var crt = new CRT();
crt.OnTimeElapsed = () =>
{
    if ((crt.Time - 20) % 40 == 0)
    {
        var v = crt.Time * crt.X;
        sum += v;
    }
};

crt.ParseFile(path);

Console.WriteLine(sum);
crt.Display();
