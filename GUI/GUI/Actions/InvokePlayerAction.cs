using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Actions
{
    using GUI.ActionReceivers;

    class InvokePlayerAction : IAction
    {
        Player player;

        public InvokePlayerAction(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.doInvokedAction();
        }
    }
}
