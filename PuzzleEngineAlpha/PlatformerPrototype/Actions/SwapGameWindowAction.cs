using System;

namespace PlatformerPrototype.Actions
{
    using Scene.Menu;

    class SwapGameWindowAction: PuzzleEngineAlpha.Actions.IAction
    {
        #region Declarations

        MenuHandler menuHandler;
        string newWindow;

        #endregion

        public SwapGameWindowAction(MenuHandler menuHandler, string newWindow)
        {
            this.menuHandler = menuHandler;
            this.newWindow = newWindow;
        }

        public void Execute()
        {
            menuHandler.SwapWindow(newWindow);
        }
    
    }
}
