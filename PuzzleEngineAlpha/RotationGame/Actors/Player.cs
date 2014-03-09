using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RotationGame.Actors
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

        #endregion

        #region Helper Methods

        public Vector2 GetVelocity(int state)
        {

            switch (state)
            {
                case 0:
                    return new Vector2(0, -step);
                case 2:
                    return new Vector2(0, +step);
                case 1:
                    return new Vector2(-step, 0);
                case 3:
                    return new Vector2(step, 0);
                default:
                    return Vector2.Zero;
            }


        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            velocity = GetVelocity(camera.RotationState);

         /**   movementScript.RotationState = camera.RotationState;

            if (movementScript.MoveUp)
            {
                velocity = new Vector2(0, -step);
                movementState = 0;
            }
            if (movementScript.MoveDown)
            {
                velocity = new Vector2(0, +step);
                movementState = 1;
            }
            if (movementScript.MoveLeft)
            {
                velocity = new Vector2(-step,0);
                movementState = 2;
            }
            if (movementScript.MoveRight)
            {
                velocity = new Vector2(step,0);
                movementState = 3;
            }*/
            base.Update(gameTime);

            if (Collided)
                location = InitialLocation;
        }

    }
}
