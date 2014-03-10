using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GateGame.Actors
{
    using PuzzleEngineAlpha.Input.Scripts;
    using PuzzleEngineAlpha.Scene;

    public class Player : PuzzleEngineAlpha.Actors.MapObject
    {

        #region Declarations

        MovementScript movementScript;
        int movementState;

        #endregion

        #region Constructor

        public Player(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, float step, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap,camera,location, frameWidth, frameHeight, collideWidth,collideHeight)
        {
            this.InitialLocation = location;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "run"));
            currentAnimation = "run";
            this.step = step;
            movementScript = new MovementScript();
            movementState = 0;
        }

        #endregion

        #region Properties

        public Vector2 InitialLocation
        {
            get;
            set;
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
                return new Vector2(location.X + collideWidth / 2, location.Y + collideHeight / 2);
            }
        }

        public Vector2 RelativeCenter
        {
            get
            {
                return new Vector2(location.X + collideWidth / 2, location.Y + collideHeight / 2) + RelativeOffset();
            }
        }

        float OffSet
        {
            get
            {
                return 70.0f;
            }
        }

        #endregion

        #region Helper Methods

        Vector2 RelativeOffset()
        {
            switch (movementState)
            {
                case 0:
                    return new Vector2(0, -OffSet);
                case 1:
                    return new Vector2(0, +OffSet);
                case 2:
                    return new Vector2(-OffSet, 0);
                case 3:
                    return new Vector2(+OffSet, 0);
                default:
                    return Vector2.Zero;

            }
        }

        void ManipulateVector(ref Vector2 vector, float maxAcceleration,float amount)
        {
            if (vector.X > 0)
                vector.X = MathHelper.Clamp(vector.X - amount, 0, maxAcceleration);
            else
                vector.X = MathHelper.Clamp(vector.X + amount, -maxAcceleration, 0);

            if (vector.Y > 0)
                vector.Y = MathHelper.Clamp(vector.Y - amount, 0, maxAcceleration);
            else
                vector.Y = MathHelper.Clamp(vector.Y + amount, -maxAcceleration, 0);
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            movementScript.RotationState = camera.RotationState;

            if (movementScript.MoveUp)
            {
                velocity += new Vector2(0, -step);
                movementState = 0;
            }
            else if (movementScript.MoveDown)
            {
                velocity +=new Vector2(0, +step);
                movementState = 1;
            }
            if (movementScript.MoveLeft)
            {
                velocity += new Vector2(-step,0);
                movementState = 2;
            }
            else if (movementScript.MoveRight)
            {
                velocity += new Vector2(step,0);
                movementState = 3;
            }
   
            ManipulateVector(ref velocity, 230.0f, 10f);

            base.Update(gameTime);

        }

    }
}
