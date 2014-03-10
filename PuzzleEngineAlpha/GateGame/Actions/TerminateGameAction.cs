using System;

namespace GateGame.Actions
{
    using Scene;

    public class TerminateGameAction : PuzzleEngineAlpha.Actions.IAction
    {
        public void Execute()
        {
            GateGame.Terminate = true;
        }
    
    }
}
