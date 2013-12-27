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
            WindowText.AddText(new Vector2(10, 10), " ");
            WindowText.AddText(new Vector2(40, 10), " ");
            WindowText.AddText(new Vector2(70, 10), " ");
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
                        mapCells[x, y] = new MapSquare(2, false, " ");
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

        //Review this Property
        int HorizontalOffset
        {
            get
            {
                double realHorizontalTiles = Math.Ceiling((double)ResolutionHandler.WindowWidth / TileWidth);
                double currentHorizontalTiles = Math.Ceiling(ResolutionHandler.WindowWidth / (TileWidth * Camera.Zoom));

                return (int)(Math.Ceiling(realHorizontalTiles - currentHorizontalTiles) / 2 - 3);
            }
        }

        int VerticalOffset
        {
            get
            {
                double realVerticalTiles = Math.Ceiling((double)ResolutionHandler.WindowHeight / TileHeight);
                double currentVerticalTiles = Math.Ceiling(ResolutionHandler.WindowHeight / (TileHeight * Camera.Zoom));

                return (int)(Math.Ceiling(realVerticalTiles - currentVerticalTiles) / 2 - 3);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            int startX = GetCellByPixelX((int)(Camera.Position.X));
            int endX = GetCellByPixelX((int)(Camera.Position.X) + ResolutionHandler.WindowWidth);

            startX += HorizontalOffset;
            endX -= HorizontalOffset;

            int startY = GetCellByPixelY((int)(Camera.Position.Y));
            int endY = GetCellByPixelY((int)(Camera.Position.Y) + ResolutionHandler.WindowHeight);
            startY += VerticalOffset;
            endY -= VerticalOffset;

            if (Camera.IsFlipped)
                DrawTiles(spriteBatch, startX, endX, startY, endY);
            else
            {
                int horizontalSize = endX - startX;
                int verticalSize = endY- startY;
                int difference = 0;
                if (horizontalSize > verticalSize)
                    difference = horizontalSize - verticalSize;
                else
                    difference = verticalSize - horizontalSize;

                DrawTiles(spriteBatch, startX- difference, endX + difference, startY - difference, endY + difference);
            }
        }

        void DrawTiles(SpriteBatch spriteBatch, int startX, int endX, int startY, int endY)
        {
            WindowText.SetText(new Vector2(10, 10), "Location: {X: " + startX + " / " + MapWidth
                                                              +"  Y: "  + startY + " / " + MapHeight + "}");
            WindowText.SetText(new Vector2(10, 40), "Scale: " + Camera.Scale);
            WindowText.SetText(new Vector2(10, 70), "Rotation: " + Camera.Rotation);

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
