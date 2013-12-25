using System;

namespace PuzzlePrototype.Level
{
    [Flags]
    public enum GateStatus
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 4,
        Yellow = 8,
        Pink = 16,
        Cornflowerblue = 32,
        All = 63
    }

}
