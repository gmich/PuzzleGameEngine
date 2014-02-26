using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    interface IMenuWindow
    {
        void Show();

        void Hide();

        bool IsFocused
        {
            get;
        }

        bool IsShown
        {
            get;
        }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
