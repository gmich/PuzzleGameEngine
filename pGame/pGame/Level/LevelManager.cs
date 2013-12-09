using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzlePrototype.Level
{
    public class LevelManager
    {

        #region Declarations

        //TODO: replace int with object
        static List<List<int>> map;


        #endregion

        #region Public Constructor

        public LevelManager()
        {
            map = new List<List<int>>();
        }

        #endregion

        #region Private Properties

        #region Map Dimensions

        public static int MapWidth
        {         
            get
            {
                return map[0].Count();
            }
        }

        public static int MapHeight
        {
            get
            {
                return map.Count()/map[0].Count();
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
    }
}
