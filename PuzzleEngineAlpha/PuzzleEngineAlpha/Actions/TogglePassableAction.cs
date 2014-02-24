namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class TogglePassableAction : IAction
    {
        #region Declarations

        MenuButton button;

        #endregion

        public TogglePassableAction(MenuButton button)
        {
            this.button = button;
        }

        public void Execute()
        {        
            Scene.Editor.ConfigurationScene.Passable = !Scene.Editor.ConfigurationScene.Passable;
            if (Scene.Editor.ConfigurationScene.Passable)
            {
                button.SetText("is passable");
            }
            else
            {
                button.SetText("not passable");
            }
        }
    }
}
