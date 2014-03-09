using System;

namespace RotationGame.Actions
{
    using Scene;

    public class TerminateGameAction : PuzzleEngineAlpha.Actions.IAction
    {
        public void Execute()
        {
            RotationGame.Terminate = true;
        }
    
    }
}
