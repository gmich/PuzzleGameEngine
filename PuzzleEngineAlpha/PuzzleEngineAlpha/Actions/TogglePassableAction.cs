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

            if (TileManager.MapSquare != null)
            {
                TileManager.MapSquare = new Level.MapSquare(TileManager.MapSquare.LayerTile, Scene.Editor.ConfigurationScene.Passable, TileManager.MapSquare.CodeValue,TileManager.MapSquare.ActorID);
            }

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
