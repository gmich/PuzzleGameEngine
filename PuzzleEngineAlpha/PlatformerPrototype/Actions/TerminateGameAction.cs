using System;

namespace PlatformerPrototype.Actions
{
    using Scene;

    public class TerminateGameAction : PuzzleEngineAlpha.Actions.IAction
    {
        public void Execute()
        {
            PlatformerPrototype.Terminate = true;
        }
    
    }
}
