namespace PuzzleEngineAlpha.Actions
{
    public class LoadMapAction : IAction
    {
        Level.MiniMap miniMap;
        Level.TileMap tileMap;

        public LoadMapAction(Level.MiniMap miniMap, Level.TileMap tileMap)
        {
            this.miniMap = miniMap;
            this.tileMap = tileMap;
        }

        public void Execute()
        {
            if (miniMap.HasLoadedMap)
            {
                this.tileMap.MapHeight = miniMap.levelInfo.MapHeight;
                this.tileMap.MapWidth = miniMap.levelInfo.MapWidth;
                this.tileMap.TileHeight = miniMap.levelInfo.TileHeight;
                this.tileMap.TileWidth = miniMap.levelInfo.TileWidth;
                this.tileMap.Initialize();
                this.tileMap.SetMapCells(miniMap.CloneMap());
            }
        }
    }
}
