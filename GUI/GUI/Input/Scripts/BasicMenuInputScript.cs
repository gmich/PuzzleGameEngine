using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Input.Scripts
{
    public class BasicMenuInputScript: IMenuInputScript
    {
        bool Next
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.EnumerateVNext);
            }
        }

        bool Previous
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.EnumerateVPrevious);
            }
        }

        bool Trigger
        {
            get
            {
                return InputHandler.IsKeyReleased(InputConfigurationManager.Config.Trigger);
            }
        }
    }
}
