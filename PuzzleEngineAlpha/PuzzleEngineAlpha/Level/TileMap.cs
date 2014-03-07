using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace PuzzleEngineAlpha.Level
{
    using Camera;
    using Diagnostics;
    using Resolution;

    public class TileMap
    {
        #region Declarations

        public int MapWidth;
        public int MapHeight;
        public MapSquare[,] mapCells;
        protected Texture2D tileSheet;
        Vector2 cameraPosition;
        public LevelInfo levelInfo;

        #endregion

        public TileMap(Vector2 cameraPosition, ContentManager Content, int sourceTileWidth, int sourceTileHeigh, int tileWidth, int tileHeight)
        {
            this.cameraPosition = cameraPosition;
            tileSheet = Content.Load<Texture2D>(@"Textures/PlatformTilesTemp");
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;

            this.SourceTileWidth = sourceTileWidth;
            this.SourceTileHeight = sourceTileHeigh;

        }

        #region Randomize Map
        
        //TODO: update randomize algorithm
        public virtual void Randomize(int mapWidth, int mapHeight)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;

            Random rand = new Random();

            mapCells = new MapSquare[MapWidth, MapHeight];

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    mapCells[x, y] = new MapSquare(2, true, "");

                }
            }
        }

        #endregion

        #region Initialize

        public void Initialize()
        {
            mapCells = new MapSquare[MapWidth, MapHeight];
            Camera.WorldSize = new Vector2(this.MapPixelWidth, this.MapPixelHeight);
            //cameraPosition = Vector2.Zero;
        }

        public virtual void SetMapCells(MapSquare[,] mapSquares)
        {
            this.mapCells = mapSquares;
        }

        #endregion

        #region Clone Map

        public MapSquare[,] CloneMap()
        {
            MapSquare[,] clonedCells = new MapSquare[MapWidth, MapHeight];

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    clonedCells[x, y] = mapCells[x, y].DeepClone();
                }
            }
            return clonedCells;
        }

        #endregion

        #region Draw Helper Methods

        public virtual Color GetColor(Rectangle rectangle)
        {
            return Color.White;
        }

        #endregion

        #region Draw Properties

        public Camera Camera
        {
            get;
            set;
        }

        public virtual int TileWidth
        {
            get;
            set;
        }

        public virtual int TileHeight
        {
            get;
            set;
        }

        protected int StartX
        {
            get
            {
                return (int)MathHelper.Max(0,GetCellByPixelX((int)(this.Camera.Position.X))-17);
            }
        }

        protected int EndX
        {
            get
            {
                return (int)MathHelper.Min(MapWidth-1, GetCellByPixelX((int)(Camera.Position.X) + ResolutionHandler.WindowWidth) + 15);
            }
        }

        protected int StartY
        {
            get
            {
                return (int)MathHelper.Max(0, GetCellByPixelY((int)(Camera.Position.Y))-17);
            }
        }

        protected int EndY
        {
            get
            {
                return (int)MathHelper.Min(MapHeight-1, GetCellByPixelY((int)(Camera.Position.Y) + ResolutionHandler.WindowHeight) + 15);
            }
        }

        public int MapPixelWidth
        {
            get
            {
                return MapWidth * TileWidth;
            }
        }

        public int MapPixelHeight
        {
            get
            {
                return MapHeight * TileHeight;
            }
        }

        #endregion

        #region Tile and Tile Sheet Handling

        public int TilesPerRow
        {
            get { return tileSheet.Width / SourceTileWidth; }
        }

        public int TilesPerColumn
        {
            get { return tileSheet.Height / SourceTileHeight; }
        }

        public int CountSheetTiles
        {
            get
            {
                return TilesPerRow * TilesPerColumn;
            }
        }

        public Rectangle TileSourceRectangle(int tileIndex)
        {
            return new Rectangle(
                (tileIndex % TilesPerRow) * SourceTileWidth,
                (tileIndex / TilesPerRow) * SourceTileHeight,
                SourceTileWidth,
                SourceTileHeight);
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

        int SourceTileWidth
        {
            get;
            set;
        }

        int SourceTileHeight
        {
            get;
            set;
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

        public Rectangle CellScreenRectangle(Vector2 location)
        {
            return Camera.WorldToScreen(CellWorldRectangle(location));
        }

        public void SetTileAtCell(int tileX, int tileY, int tileIndex)
        {
            if ((tileX >= 0) && (tileX < MapWidth) &&
                (tileY >= 0) && (tileY < MapHeight))
            {
                mapCells[tileX, tileY].LayerTile = tileIndex;
            }
        }

        public MapSquare GetMapSquareAtPixel(int pixelX, int pixelY)
        {
            return GetMapSquareAtCell(GetCellByPixelX(pixelX), GetCellByPixelY(pixelY));
        }

        public MapSquare GetMapSquareAtPixel(Vector2 pixelLocation)
        {
            return GetMapSquareAtPixel((int)pixelLocation.X,(int)pixelLocation.Y);
        }

        #endregion

        #region Draw


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Scene.Editor.DiagnosticsScene.SetText(new Vector2(10, 10), "Location: {X: " + StartX + " / " + MapWidth + "  Y: " + StartY + " / " + MapHeight + "}");
            Scene.Editor.DiagnosticsScene.SetText(new Vector2(10, 40), "Scale: " + Camera.Zoom);
            Scene.Editor.DiagnosticsScene.SetText(new Vector2(10, 70), "Rotation: " + Camera.Rotation);  
         
       /*     int horizontalSize = EndX - StartX;
            int verticalSize = EndY - StartY;
            int difference = 0;
            if (horizontalSize > verticalSize)
                difference = horizontalSize - verticalSize;
            else
                difference = verticalSize - horizontalSize;*/

            int difference = 0;
            DrawTiles(spriteBatch, StartX - difference, EndX + difference, StartY - difference, EndY + difference);



        }

        void DrawTiles(SpriteBatch spriteBatch, int startX, int endX, int startY, int endY)
        {   
            for (int x = startX; x <= endX; x++)
                for (int y = startY; y <= endY; y++)
                {
                    if ((x >= 0) && (y >= 0) &&
                        (x < MapWidth) && (y < MapHeight))
                    {
                        spriteBatch.Draw(tileSheet, CellScreenRectangle(x, y), TileSourceRectangle(mapCells[x, y].LayerTile),
                          GetColor(CellScreenRectangle(x, y)), 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Tile);
                    }
                }
        }
        
        #endregion

    }
}
