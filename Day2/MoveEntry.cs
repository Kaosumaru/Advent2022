namespace Day2
{
    public struct MoveEntry
    {
        public enum MoveType
        {
            Rock = 0,
            Paper,
            Scisors
        }

        const int MoveCount = 3;

        public MoveType Opponent;
        public MoveType Me;

        public static MoveType WinningResponseFor(MoveType move)
        {
            return (MoveType)(((int)move + 1) % MoveCount);
        }

        public static MoveType DrawResponseFor(MoveType move)
        {
            return move;
        }
        public static MoveType LosingResponseFor(MoveType move)
        {
            var i = (int)move - 1;
            if (i < 0)
                i = MoveCount - 1;

            return (MoveType)i;
        }

        public bool IsADraw()
        {
            return DrawResponseFor(Opponent) == Me;
        }

        public bool DidIWin()
        {
            return WinningResponseFor(Opponent) == Me;
        }

    }

}
