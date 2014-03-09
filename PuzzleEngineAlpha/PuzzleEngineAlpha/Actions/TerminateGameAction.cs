using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene;

    public class TerminateGameAction : IAction
    {
        public void Execute()
        {
            Engine.Terminate = true;
        }
    
    }
}
