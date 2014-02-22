using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
{
    public class Frame
    {
        #region Public Constructors

        public Frame()
        {
            this.Passable = true;
            this.CodeValue = "";
            this.LayerTile = 0;
        }

        public Frame(int layerTile, bool passable, string codeValue)
        {
            this.Passable = passable;
            this.CodeValue = codeValue;
            this.LayerTile = layerTile;
        }

        #endregion

        #region Properties

        public int LayerTile
        {
            get;
            set;
        }

        public bool Passable
        {
            get;
            set;
        }

        public string CodeValue
        {
            get;
            set;
        }

        #endregion
    }
}
