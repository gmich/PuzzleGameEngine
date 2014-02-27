using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene;

    class GoToEditorAction: IAction
    {
        public void Execute()
        {
            SceneDirector.ToggleMenuTrigger = true;
        }
    
    }
}
