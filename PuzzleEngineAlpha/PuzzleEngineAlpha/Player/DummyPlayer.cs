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
        int movementState;

        public DummyPlayer(Vector2 location, Texture2D texture,float step)
        {
            this.location = location;
            this.texture = texture;
            this.step = step;
            movementScript = new MovementScript();
            movementState = 0;
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

        public Vector2 RelativeCenter
        {
            get
            {
                return new Vector2(location.X + texture.Width / 2, location.Y + texture.Height / 2) + RelativeOffset();
            }
        }


        float OffSet
        {
            get
            {
                return 70.0f;
            }
        }
        Vector2 RelativeOffset()
        {
            switch (movementState)
            {
                case 0:
                    return new Vector2(0,-OffSet);
                case 1:
                    return new Vector2(0,+OffSet);
                case 2:
                    return new Vector2(-OffSet,0);
                case 3:
                    return new Vector2(+OffSet,0);
                default:
                    return Vector2.Zero;

            }
        }
        public void Update(GameTime gameTime)
        {
            movementScript.RotationState = Camera.RotationState;

            if (movementScript.MoveUp)
            {
                location -= new Vector2(0, step);
                movementState = 0;
            }
            if (movementScript.MoveDown)
            {
                location -= new Vector2(0, -step);
                movementState = 1;
            }
            if (movementScript.MoveLeft)
            {
                location -= new Vector2(step, 0);
                movementState = 2;
            }
            if (movementScript.MoveRight)
            {
                location -= new Vector2(-step, 0);
                movementState = 3;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location - Camera.Position, Color.White);
        }
    }
}
