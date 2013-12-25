using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzlePrototype.Level.Parser
{
    interface ILeveLParser
    {
        bool LoadMap(ref Level level);

        bool SaveMAp(ref Level level);
    }
}
