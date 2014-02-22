using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Input.Scripts
{
    public class IEditorInputScript
    {
        public bool Click
        {
            get;
        }

        public bool Release
        {
            get;
        }

        public Rectangle Area
        {
            get;
            set;
        }
    }
}
