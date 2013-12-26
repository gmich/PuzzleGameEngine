using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DynamicCamera
{
    using Input;
    using Scene;

    //Helper dummy player
    public class DummyPlayer
    {
        public Vector2 location;
        Texture2D texture;

        public DummyPlayer(Vector2 location, Texture2D texture)
        {
            this.location = location;
            this.texture = texture;
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(location.X + texture.Width / 2, location.Y + texture.Height / 2);
            }
        }
            
        public void Update(GameTime gameTime)
        {
            float step = 10;
            if(InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                location -=new Vector2(0,step);
            }
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                location -= new Vector2(0,-step);
            }
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                location -= new Vector2(step,0);
            }
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                location -= new Vector2(-step,0);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location - GameScene.CameraLocation, Color.White);
        }
    }
}
