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
                TileManager.SourceRectangle = button.SourceRectangle;
                TileManager.MapSquare = new Level.MapSquare(button.MapSquare.LayerTile, button.MapSquare.Passable, button.MapSquare.CodeValue);
            }
        }
    }
}
