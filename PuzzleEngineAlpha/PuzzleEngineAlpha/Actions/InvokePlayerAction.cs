using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Actions
{
    using PuzzleEngineAlpha.ActionReceivers;

    class InvokePlayerAction : IAction , IEquatable<IAction>
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

        #region Implement Equality

        public bool Equals(IAction otherAction)
        {
            return (this.GetType().Name == otherAction.GetType().Name);
        }

        #endregion
    }
}
