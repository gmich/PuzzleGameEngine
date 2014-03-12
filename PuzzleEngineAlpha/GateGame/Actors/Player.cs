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
        readonly ActorManager actorManager;

        #endregion

        #region Constructor

        public Player(ActorManager actorManager,PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, float step, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap,camera,location, frameWidth, frameHeight, collideWidth,collideHeight)
        {
            this.InitialLocation = location;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "run"));
            currentAnimation = "run";
            this.step = step;
            movementScript = new MovementScript();
            movementState = 0;
            this.actorManager = actorManager;
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
                return 60.0f;
            }
        }

        #endregion

        #region Helper Methods

        Vector2 RelativeOffset()
        {
          /*  switch (movementState)
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
            */
            return Velocity;
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

        void ToggleGate()
        {
            Button button = actorManager.GetInteractionButton(this.CollisionRectangle);

            if (button != null)
            {
                actorManager.ToggleGatesWithTag(button.Tag);
            }
        }

        void AdjustLocationInMap()
        {
            this.location.X = MathHelper.Clamp(this.location.X, 0, camera.WorldSize.X);
            this.location.Y = MathHelper.Clamp(this.location.Y, 0, camera.WorldSize.Y);
        }

        #endregion

        #region Collision Detection
        
        public override void HorizontalActorCollision(ref Vector2 moveAmount,Vector2 corner1, Vector2 corner2)
        {

            if (actorManager.HasActorAtLocation(corner1) || actorManager.HasActorAtLocation(corner2))
            {
                moveAmount.X = 0.0f;
                velocity.X=0.0f;
                Collided=true;
            }
        }

        public override void VerticalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1) || actorManager.HasActorAtLocation(corner2))
            {
                moveAmount.Y = 0.0f;
                velocity.Y = 0.0f;
                Collided = true;
            }
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

            //TODO: get input from input configuration
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(Keys.Space))
            {
                ToggleGate();
            }

            actorManager.IntersectsWithCoin(this.CollisionRectangle);

            ManipulateVector(ref velocity, 240.0f, 10f);

            base.Update(gameTime);

            AdjustLocationInMap();
        }


    }
}
