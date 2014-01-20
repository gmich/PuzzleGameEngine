using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Components
{
    using Actions;

    interface IGUIArea
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        void StoreAndExecute(IAction action);

        Vector2 Position
        {
            get;
        }
    }
}
