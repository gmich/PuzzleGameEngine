using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleEngineAlpha.Actions
{
    using Scene.Editor;

    class ToggleSelectionAction :IAction
    {
        SelectionScene selectionScene;
        SelectionScene selectionSceneActors;
        bool showActors;

        public ToggleSelectionAction(SelectionScene selectionScene, SelectionScene selectionSceneActors, bool showActors)
        {
            this.selectionScene = selectionScene;
            this.showActors = showActors;
            this.selectionSceneActors = selectionSceneActors;
        }

        public void Execute()
        {
            this.selectionScene.IsActive = !showActors;
            this.selectionScene.DrawScene = !showActors;
            this.selectionSceneActors.DrawScene = showActors;
            this.selectionSceneActors.IsActive = showActors;
        }
    
    }
}