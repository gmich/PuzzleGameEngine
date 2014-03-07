namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class SetSelectedTileAction : IAction
    {
        #region Declarations

        TileButton button;

        #endregion

        public SetSelectedTileAction(TileButton button)
        {
            this.button = button;
        }

        public void Execute()
        {
            TileManager.TileSourceRectangle = button.SourceRectangle;
            TileManager.MapSquare = new Level.MapSquare(button.LayerTile, Scene.Editor.ConfigurationScene.Passable, Scene.Editor.ConfigurationScene.TextBoxText,-1);
        }
    }
}
