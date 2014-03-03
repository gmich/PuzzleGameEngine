using System;

namespace PuzzleEngineAlpha.Actions
{
    using Components.TextBoxes;

    class NewMapAction :IAction
    {
        #region Declarations

        TextBox initialID;
        TextBox tileSize;
        TextBox mapWidth;
        TextBox mapHeight;
        Level.TileMap tileMap;

        #endregion

        public NewMapAction(TextBox initialID, TextBox tileSize, TextBox mapWidth, TextBox mapHeight, Level.TileMap tileMap)
        {
            this.initialID = initialID;
            this.tileSize = tileSize;
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
            this.tileMap = tileMap;
        }

        public void Execute()
        {
            tileMap.TileHeight = Convert.ToInt32(tileSize.Text);
            tileMap.TileWidth =  Convert.ToInt32(tileSize.Text);
            tileMap.MapWidth =  Convert.ToInt32(mapWidth.Text);
            tileMap.MapHeight = Convert.ToInt32(mapHeight.Text);
            tileMap.Initialize();
            tileMap.SetMapCells(GenerateMapSquares(tileMap.MapWidth,tileMap.MapHeight,Convert.ToInt32(initialID.Text)));
        }

        Level.MapSquare[,] GenerateMapSquares(int mapwidth,int mapheight,int value)
        {
            if (value > tileMap.CountSheetTiles)
                value = 0;

            Level.MapSquare[,] mapSquares = new Level.MapSquare[mapwidth,mapheight];
            for (int i = 0; i < mapwidth; i++)
            {
                for (int j = 0; j < mapheight; j++)
                    mapSquares[i, j] = new Level.MapSquare(value, true, "");
            }
            return mapSquares;
        }
    }
}
