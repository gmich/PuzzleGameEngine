using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Animations
{
    public interface IAnimation
    {
        #region Properties

        bool IsAlive
        {
            get;
        }

        string Text
        {
            set;
        }

        Vector2 Location
        {
            get;
            set;
        }

        #endregion

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
