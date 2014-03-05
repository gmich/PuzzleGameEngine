using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene
{
    public interface IScene
    {

        bool IsActive
        {
            get;
            set;
        }

        void GoInactive();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
