using System;

namespace PuzzleEngineAlpha.Actions
{
    using Scene.Editor.Menu;

    class SwapEditorWindowAction: IAction
    {
        #region Declarations

        MenuHandler menuHandler;
        string newWindow;

        #endregion

        public SwapEditorWindowAction(MenuHandler menuHandler,string newWindow)
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
