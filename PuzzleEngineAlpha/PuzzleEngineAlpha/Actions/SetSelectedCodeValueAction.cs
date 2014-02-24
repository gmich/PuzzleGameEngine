namespace PuzzleEngineAlpha.Actions
{
    using Components.TextBoxes;
    using Level.Editor;

    public class SetSelectedCodeValueAction : IAction
    {
        #region Declarations

        TextBox textbox;

        #endregion

        public SetSelectedCodeValueAction(TextBox textbox)
        {
            this.textbox = textbox;
        }

        public void Execute()
        {
            if (TileManager.MapSquare != null)
            {
                TileManager.MapSquare = new Level.MapSquare(TileManager.MapSquare.LayerTile, TileManager.MapSquare.Passable, textbox.Text);
            }
        }
    }
}
