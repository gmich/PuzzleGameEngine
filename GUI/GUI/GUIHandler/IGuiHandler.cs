using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.GUIHandler
{
    using Actions;
    using Components;
    using ActionReceivers;
    //example class

    public class GUIHandler
    {
        public void Initialize()
        {
            IGUIArea button = new MenuButton();
            Player player = new Player();
            button.StoreAndExecute(new InvokePlayerAction(player));

            //button.update and draw
        }
    }
}
