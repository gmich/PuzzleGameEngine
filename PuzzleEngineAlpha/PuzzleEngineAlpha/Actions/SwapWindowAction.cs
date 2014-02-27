using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene.Editor.Menu;

    class SwapWindowAction: IAction
    {
        #region Declarations

        MenuHandler menuHandler;
        string newWindow;

        #endregion

        public SwapWindowAction(MenuHandler menuHandler,string newWindow)
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
