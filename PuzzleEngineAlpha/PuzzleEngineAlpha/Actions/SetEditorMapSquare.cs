namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class SetEditorMapSquare : IAction
    {
        #region Declarations

        EditorMapSquare button;

        #endregion

        public SetEditorMapSquare(EditorMapSquare button)
        {
            this.button = button;
        }

        public void Execute()
        {
            if (TileManager.MapSquare != null)
            {
                button.MapSquare.LayerTile = TileManager.MapSquare.LayerTile;
                button.MapSquare.CodeValue = TileManager.MapSquare.CodeValue;
                button.MapSquare.Passable = TileManager.MapSquare.Passable;

                button.SourceRectangle = TileManager.SourceRectangle;
            }
        }
    }
}
