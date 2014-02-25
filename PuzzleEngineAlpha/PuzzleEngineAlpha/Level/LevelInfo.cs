using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Level
{
    public class LevelInfo
    {

        public LevelInfo(string serializationMethod, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.SerializationMethod = serializationMethod;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
        }

        #region Public Properties

        public string SerializationMethod
        {
            get;
            set;
        }

        public int MapWidth
        {
            get;
            set;
        }

        public int MapHeight
        {
            get;
            set;
        }

        public int TileWidth
        {
            get;
            set;
        }

        public int TileHeight
        {
            get;
            set;
        }

        #endregion

    }
}
