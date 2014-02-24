namespace PuzzleEngineAlpha.Actions
{
    using Components.Buttons;
    using Level.Editor;

    public class UnSelectedTileAction : IAction
    {
        public void Execute()
        {
            TileManager.MapSquare = null;
        }
    }
}
