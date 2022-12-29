namespace Day7
{
    internal struct File
    {
        public string Name;
        public int Size;

        public static File Create(string info)
        {
            var s = info.Split(' ');
            return new File
            {
                Name = s[1],
                Size = int.Parse(s[0])
            };
        }
    }
}
