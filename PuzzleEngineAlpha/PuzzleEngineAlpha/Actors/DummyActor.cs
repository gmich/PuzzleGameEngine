using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors
{
    public class DummyActor: IActor
    {
        Texture2D texture;
        Rectangle destinationRectangle;

        public DummyActor(Texture2D texture, Rectangle destinationRectangle)
        {
            this.texture = texture;
            this.destinationRectangle = destinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }
    }
}
