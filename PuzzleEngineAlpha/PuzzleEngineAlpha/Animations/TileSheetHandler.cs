using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PuzzleEngineAlpha.Animations
{
    class TileSheetHandler
    {

        #region Constructor

        public TileSheetHandler(Texture2D tileSheet, int TileWidth,int TileHeight)
        {
            this.TileSheet=tileSheet;
            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;

        }

        #endregion

        #region Rendering Properties

        public Texture2D TileSheet
        {
            get;
            set;
        }

        int TileWidth
        {
            get;
            set;
        }

        int TileHeight
        {
            get;
            set;
        }

        #endregion

        #region Tile and Tile Sheet Handling

        public int TilesPerRow
        {
            get { return TileSheet.Width / TileWidth; }
        }

        public int TilesPerColumn
        {
            get { return TileSheet.Height / TileHeight; }
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
                (tileIndex % TilesPerRow) * TileWidth,
                (tileIndex / TilesPerRow) * TileHeight,
                TileWidth,
                TileHeight);
        }

        #endregion


    }
}
