using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
{
    using Components.Buttons;
    using Components;

    public class EditorMapSquare : TileButton
    {
        #region Public Constructors

        public EditorMapSquare(DrawProperties buttonDrawProperties, DrawProperties frameDrawProperties, Vector2 position, Vector2 size,Rectangle sourceRectangle,Camera.Camera camera, Rectangle generalArea,int layerTile)
            : base(buttonDrawProperties,frameDrawProperties,position,size,sourceRectangle,camera,generalArea,layerTile)
        {

        }

        #endregion

    }
}
