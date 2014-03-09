using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene;

    public class ExitMenuAction: IAction
    {
        public void Execute()
        {
            SceneDirector.ToggleMenuTrigger = true;
        }
    
    }
}
