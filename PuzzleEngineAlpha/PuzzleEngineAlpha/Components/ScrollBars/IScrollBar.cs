using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.ScrollBars
{
    interface IScrollBar
    {
        Camera.Camera Camera
        {
            get;
        }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}