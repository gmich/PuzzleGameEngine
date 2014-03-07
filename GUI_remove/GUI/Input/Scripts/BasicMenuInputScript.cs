using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Input.Scripts
{
    public class BasicMenuInputScript: IMenuInputScript
    {
        public bool Next
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.EnumerateVNext);
            }
        }

        public bool Previous
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.EnumerateVPrevious);
            }
        }

        public bool Trigger
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.Trigger);
            }
        }
    }
}
