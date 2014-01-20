using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DynamicCamera.Animations
{
    interface IAnimation
    {
        void Remove(IAnimation animation);

        void Add(IAnimation animation);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
