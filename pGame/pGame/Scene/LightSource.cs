using System;
using Microsoft.Xna.Framework;

namespace PuzzlePrototype.Scene
{
   

    public class LightSource
    {
        public LightSource(GameObject source, int size,Color color)
        {
            this.Source = source;
            this.Size = size;
            this.Color = color;
        }

        #region Public Properties

        public Color Color
        {
            get;
            set;
        }

        public Vector2 OffSet
        {
            get;
            set;
        }
            
        public int Size
        {
            get;
            set;
        }
        public GameObject Source
        {
            get;
            set;
        }

        public Vector2 MapLocation
        {
            get
            {
                return Source.MapLocation;
            }
        }

        #endregion

    }
}
