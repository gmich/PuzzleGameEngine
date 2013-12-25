using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzlePrototype.Level
{
    public class Level
    {
        #region Declarations

        List<List<Tile>> map;

        #endregion

        #region Public Constructor

        public Level()
        {
        }

        #endregion

        #region Map Properties

        public int Width
        {
            get
            {
                return  (map[0].Count());
            }
        }

        public int Height
        {
            get
            {
                return (map.Count() / map[0].Count());
            }
        }

        #endregion

        #region Information about Tiles

        public bool IsSquarePassable(int x,int y)
        {
            return map[x][y].Passable;
        }

        #endregion

        #region Map Configuration

        public void SetMap(List<List<Tile>> map)
        {
            this.map = map;
        }

        #endregion
    }
}
