using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene
{
    interface IScene
    {
        bool IsActive
        {
            get;
            set;
        }

        void Update(GameTime gameTime);

        void UpdateRenderTarget();

        void Draw(SpriteBatch spriteBatch);
    }
}
