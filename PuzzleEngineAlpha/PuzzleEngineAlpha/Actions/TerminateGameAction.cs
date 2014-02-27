using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene;

    class TerminateGameAction : IAction
    {
        public void Execute()
        {
            Engine.Terminate = true;
        }
    
    }
}
