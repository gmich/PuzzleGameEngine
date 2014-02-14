using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DynamicCamera.Player
{
    using Input;
    using Scene;

    //Helper dummy player
    public class DummyPlayer
    {
        public Vector2 location;
        Texture2D texture;
        Keys up;
        Keys down;
        Keys left;
        Keys right;
        int state;

        public DummyPlayer(Vector2 location, Texture2D texture,float step)
        {
            this.location = location;
            this.texture = texture;
            this.step = step;
            state = 0;
            InitializeKeys(state);
        }

        public void InitializeKeys(int state)
        {
            this.state = state;
            switch (state)
            {
                case 1:
                    up = Keys.Right;
                    down = Keys.Left;
                    left = Keys.Up;
                    right = Keys.Down;
                    break;
                case 2:
                    up = Keys.Down;
                    down = Keys.Up;
                    left = Keys.Right;
                    right = Keys.Left;
                    break;
                case 3:
                    up = Keys.Left;
                    down = Keys.Right;
                    left = Keys.Down;
                    right = Keys.Up;
                    break;
                default:
                    up = Keys.Up;
                    down = Keys.Down;
                    left = Keys.Left;
                    right = Keys.Right;
                    break;

            }
        }

        float step
        {
            get;
            set;
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
            if(InputHandler.IsKeyDown(up))
            {
                location -=new Vector2(0,step);
            }
            if (InputHandler.IsKeyDown(down))
            {
                location -= new Vector2(0,-step);
            }
            if (InputHandler.IsKeyDown(left))
            {
                location -= new Vector2(step,0);
            }
            if (InputHandler.IsKeyDown(right))
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
