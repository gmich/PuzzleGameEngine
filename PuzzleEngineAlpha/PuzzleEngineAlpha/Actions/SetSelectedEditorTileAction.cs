namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class SetEditorSelectedTileAction : IAction
    {
        #region Declarations

        EditorMapSquare button;

        #endregion

        public SetEditorSelectedTileAction(EditorMapSquare button)
        {
            this.button = button;
        }

        public void Execute()
        {
            if (TileManager.MapSquare == null)
            {
                if (TileManager.ShowActors)
                {
                    if (button.MapSquare.ActorID == -1) return;
                    TileManager.ActorSourceRectangle = button.ActorSourceRectangle;
                    TileManager.MapSquare = new Level.MapSquare(-1, button.MapSquare.Passable, button.MapSquare.CodeValue, button.MapSquare.ActorID);
                }
                else
                {
                    TileManager.TileSourceRectangle = button.SourceRectangle;
                    TileManager.MapSquare = new Level.MapSquare(button.MapSquare.LayerTile, button.MapSquare.Passable, button.MapSquare.CodeValue,-1);
                }
            }
        }
    }
}
