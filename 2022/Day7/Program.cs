using Day7;

string path = "../../../data2.txt";

using (Parser parser = new(path))
{
    var root = parser.Root();
    var total = root.GetSubdirectories()
        .Select(dir => dir.TotalSize())
        .Where(size => size <= 100000)
        .Sum();

    Console.WriteLine(total);

    var totalDiskSize = 70000000;
    var spaceNeededForUpdate = 30000000;
    var spaceTaken = root.TotalSize();
    var freeSpace = totalDiskSize - spaceTaken;
    var amountOfSpaceToBeFreed = spaceNeededForUpdate - freeSpace;

    Console.WriteLine($"amountOfSpaceToBeFreed={amountOfSpaceToBeFreed}");

    var smallestDirToDelete = root.GetSubdirectories()
                .Select(dir => dir.TotalSize())
                .Where(size => size >= amountOfSpaceToBeFreed)
                .Min();

    Console.WriteLine(smallestDirToDelete);
}