using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Player
{
    using Input.Scripts;
    using Scene;

    //Helper dummy player
    public class DummyPlayer
    {
        public Vector2 location;
        Texture2D texture;
        MovementScript movementScript;

        public DummyPlayer(Vector2 location, Texture2D texture,float step)
        {
            this.location = location;
            this.texture = texture;
            this.step = step;
            movementScript = new MovementScript();
        }

        float step
        {
            get;
            set;
        }        

        public PuzzleEngineAlpha.Camera.Camera Camera
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
            movementScript.RotationState = Camera.RotationState;

            if (movementScript.MoveUp)
            {
                location -=new Vector2(0,step);
            }
            if (movementScript.MoveDown)
            {
                location -= new Vector2(0,-step);
            }
            if (movementScript.MoveLeft)
            {
                location -= new Vector2(step,0);
            }
            if (movementScript.MoveRight)
            {
                location -= new Vector2(-step,0);
            }

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location - Camera.Position, Color.White);
        }
    }
}
