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
                button.MapSquare = TileManager.MapSquare;
                button.SourceRectangle = TileManager.SourceRectangle;
            }
        }
    }
}
