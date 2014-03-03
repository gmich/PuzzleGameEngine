using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
{
    [Serializable]
    public class MapSquare
    {
        #region Public Constructors

        public MapSquare()
        {
            this.Passable = true;
            this.CodeValue = "";
            this.LayerTile = 0;
        }

        public MapSquare(int layerTile, bool passable, string codeValue)
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

        #region Clone

        public MapSquare DeepClone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);

            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();

            return obj as MapSquare;
        }

        public MapSquare Clone()
        {
            return new MapSquare(this.LayerTile, this.Passable, this.CodeValue);

        }
        #endregion
    }
}
