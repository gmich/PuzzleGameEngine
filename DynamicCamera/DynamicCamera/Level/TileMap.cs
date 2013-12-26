using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DynamicCamera.Level
{

    public class TileMap
    {
        #region Declarations

        public int TileWidth;
        public int TileHeight;
        int MapWidth;
        int MapHeight;
        MapSquare[,] mapCells;
        Texture2D tileSheet;
        Vector2 cameraPosition;
        Camera Camera;
        #endregion

        public TileMap(Vector2 cameraPosition, Camera camera, Texture2D tileTexture, int tileWidth, int tileHeight)
        {
            this.cameraPosition = cameraPosition;
            tileSheet = tileTexture;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.Camera = camera;
        }


        #region Randomize Map
        
        //TODO: update randomize algorithm
        public void Randomize(int mapWidth, int mapHeight)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;

            Random rand = new Random();
            
            mapCells = new MapSquare[MapWidth, MapHeight];

            for (int x = 0; x < MapWidth; x++)
            {

                for (int y = 0; y < MapHeight; y++)
                {
                    if (rand.Next(0,2)==1)
                    {
                        mapCells[x, y] = new MapSquare(1, false, " ");
                    }
                    else
                    {
                        mapCells[x, y] = new MapSquare();
                    }
                }
            }
        }

        #endregion

        #region Tile and Tile Sheet Handling

        public int TilesPerRow
        {
            get { return tileSheet.Width / TileWidth; }
        }

        public Rectangle TileSourceRectangle(int tileIndex)
        {
            return new Rectangle(
                (tileIndex % TilesPerRow) * TileWidth,
                (tileIndex / TilesPerRow) * TileHeight,
                TileWidth,
                TileHeight);
        }

        #endregion

        #region Information about Map Cells

        public int GetCellByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        public int GetCellByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        public Vector2 GetCellByPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetCellByPixelX((int)pixelLocation.X),
                GetCellByPixelY((int)pixelLocation.Y));
        }

        public Vector2 GetCellCenter(int cellX, int cellY)
        {
            return new Vector2(
                (cellX * TileWidth) + (TileWidth / 2),
                (cellY * TileHeight) + (TileHeight / 2));
        }

        public Vector2 GetCellCenter(Vector2 cell)
        {
            return GetCellCenter(
                (int)cell.X,
                (int)cell.Y);
        }

        public Rectangle CellWorldRectangle(int cellX, int cellY)
        {
            return new Rectangle(
                cellX * TileWidth,
                cellY * TileHeight,
                TileWidth,
                TileHeight);
        }


        public Rectangle CellWorldRectangle(Vector2 cell)
        {
            return CellWorldRectangle(
                (int)cell.X,
                (int)cell.Y);
        }

        public bool CellIsPassable(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);

            if (square == null)
                return false;
            else
                return square.Passable;
        }

        public bool CellIsPassable(Vector2 cell)
        {
            return CellIsPassable((int)cell.X, (int)cell.Y);
        }


        public bool CellIsPassableByPixel(Vector2 pixelLocation)
        {
            return CellIsPassable(
                GetCellByPixelX((int)pixelLocation.X),
                GetCellByPixelY((int)pixelLocation.Y));
        }

        public string CellCodeValue(int cellX, int cellY)
        {
            MapSquare square = GetMapSquareAtCell(cellX, cellY);

            if (square == null)
                return "";
            else
                return square.CodeValue;
        }

        public string CellCodeValue(Vector2 cell)
        {
            return CellCodeValue((int)cell.X, (int)cell.Y);
        }
        #endregion

        #region Information about MapSquare objects

        public MapSquare GetMapSquareAtCell(int tileX, int tileY)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                return mapCells[tileX, tileY];
            }
            else
            {
                return null;
            }
        }

        public void SetMapSquareAtCell(
           int tileX,
           int tileY,
           MapSquare tile)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapCells[tileX, tileY] = tile;
            }
        }

        public Rectangle CellScreenRectangle(int cellX, int cellY)
        {
            return Camera.WorldToScreen(CellWorldRectangle(cellX, cellY));
        }

        public void SetTileAtCell(
           int tileX,
           int tileY,
           int tileIndex)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapCells[tileX, tileY].LayerTile = tileIndex;
            }
        }

        public MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(
                GetCellByPixelX(pixelX),
                GetCellByPixelY(pixelY));
        }

        public MapSquare GetMapSquareAtPixel(Vector2 pixelLocation)
        {
            return GetMapSquareAtPixel(
                (int)pixelLocation.X,
                (int)pixelLocation.Y);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {

            int startX = GetCellByPixelX((int)(Camera.Position.X * Camera.Scale));
            int endX = GetCellByPixelX((int)(Camera.Position.X  * (1.0f/ Camera.Scale))+ ResolutionHandler.WindowWidth);

            int startY = GetCellByPixelY((int)(Camera.Position.Y * Camera.Scale));
            int endY = GetCellByPixelY((int)(Camera.Position.Y * (1.0f / Camera.Scale)) + ResolutionHandler.WindowHeight);

            for (int x = startX; x <= endX; x++)
                for (int y = startY; y <= endY; y++)
                {
                    if ((x >= 0) && (y >= 0) &&
                        (x < MapWidth) && (y < MapHeight))
                    {
                        spriteBatch.Draw(tileSheet, CellScreenRectangle(x, y), TileSourceRectangle(mapCells[x, y].LayerTile),
                          Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                    }
                }
        }
        
        #endregion

    }
}
