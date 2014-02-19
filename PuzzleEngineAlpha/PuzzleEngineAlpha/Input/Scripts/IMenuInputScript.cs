using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Input.Scripts
{
    public interface IMenuInputScript
    {
        bool Next
        {
            get;
        }

        bool Previous
        {
            get;
        }

        bool Trigger
        {
            get;
        }
    }
}
