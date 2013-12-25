using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PuzzlePrototype.Level
{
    public class LevelManager
    {
        #region Declarations

        //TODO: replace int with object
        static Level level;

        #endregion

        #region Public Constructor

        public LevelManager()
        {
            level = new Level();
        }

        #endregion

        #region Private Properties

        #region Map Dimensions

        public static int MapWidth
        {         
            get
            {
                return level.Width;
            }
        }

        public static int MapHeight
        {
            get
            {
                return level.Height;
            }
        }

        #endregion

        #region Tile Properties

        public static int TileWidth
        {
            get;
            set;
        }

        public static int TileHeight
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Information about Tiles

        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        #endregion

        static bool SquareIsPassable(Vector2 location)
        {
            return true;
        }
    }
}
