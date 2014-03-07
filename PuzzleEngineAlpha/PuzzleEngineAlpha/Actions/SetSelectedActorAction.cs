namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class SetSelectedActorAction : IAction
    {
        #region Declarations

        TileButton button;

        #endregion

        public SetSelectedActorAction(TileButton button)
        {
            this.button = button;
        }

        public void Execute()
        {
            TileManager.ActorSourceRectangle = button.SourceRectangle;

            TileManager.MapSquare = new Level.MapSquare(-1, Scene.Editor.ConfigurationScene.Passable, Scene.Editor.ConfigurationScene.TextBoxText,button.LayerTile);
        }
    }
}
