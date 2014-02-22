using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Input.Scripts
{
    public class EditorInputScript : IMenuInputScript
    {
        public bool Click
        {
            get
            {
                return InputHandler.IsKeyReleased(ConfigurationManager.Config.EnumerateVNext);
            }
        }

        public bool Relaese
        {
            get
            {
                return InputHandler.IsKeyReleased(ConfigurationManager.Config.EnumerateVPrevious);
            }
        }

        public bool Trigger
        {
            get
            {
                return InputHandler.IsKeyReleased(ConfigurationManager.Config.Trigger);
            }
        }
    }
}
