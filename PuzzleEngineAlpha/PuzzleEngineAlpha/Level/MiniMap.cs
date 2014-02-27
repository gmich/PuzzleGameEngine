using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
{
    using Databases.Level;

    class MiniMap
    {
        #region Declarations

        RenderTarget2D miniMap;

        #endregion

        #region Constructor

        public MiniMap(GraphicsDevice graphicsDevice,Vector2 size,IMapDB mapDB,ILevelInfoDB levelInfoDB,FileStream fileStream)
        {
            miniMap = new RenderTarget2D(graphicsDevice, (int)size.X, (int)size.Y);
        }

        #endregion

        #region Properties


        #endregion
    }
}
