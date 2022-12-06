public struct Range
{
    public int Start;
    public int End;

    public bool Contains(Range other)
    {
        return Start >= other.Start && End <= other.End;
    }

    public bool Overlaps(Range other)
    {
        return Start <= other.End && End >= other.Start;
    }
}