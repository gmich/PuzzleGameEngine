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
        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;

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
            this.IsActive = false;
            tranparencyTransition = new PuzzleEngineAlpha.Animations.SmoothTransition(1.0f, 0.001f, 0.6f, 1.0f);
            IsActive=false;
            Interaction = false;
        }

        #endregion

        #region Properties

        public bool Interaction
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }

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
        
        public void HandleTransparency(GameTime gameTime)
        {
            if (IsActive)
                tranparencyTransition.Increase(gameTime);
            else
                tranparencyTransition.Decrease(gameTime);

            Transparency = tranparencyTransition.Value;
        }

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
            
           // return Velocity;
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

        public override void HorizontalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1-moveAmount,this) || actorManager.HasActorAtLocation(corner2-moveAmount,this))
            {
                moveAmount.X = 0;
                velocity.X = 0;
                Collided = true;
                return;
            }

            if (actorManager.HasActorAtLocation(corner1, this))
            {
                HorizontalCollision(actorManager.GetActorLocation(corner1, this), corner1, ref moveAmount);
            }
            if (actorManager.HasActorAtLocation(corner2, this))
            {
                HorizontalCollision(actorManager.GetActorLocation(corner2, this), corner2, ref moveAmount);
            }
        }

        public override void VerticalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1 - moveAmount, this) || actorManager.HasActorAtLocation(corner2 - moveAmount, this))
            {
                moveAmount.Y = 0;
                velocity.Y = 0;
                Collided = true;
                return;
            }

            if (actorManager.HasActorAtLocation(corner1,this))
            {
                 VerticalCollision(actorManager.GetActorLocation(corner1,this),corner1,ref moveAmount);
            }
            if(actorManager.HasActorAtLocation(corner2,this))
            {
                VerticalCollision(actorManager.GetActorLocation(corner2, this),corner2, ref moveAmount);
            }
        }

        void VerticalCollision(Vector2 actorLocation, Vector2 corner, ref Vector2 moveAmount)
        {
            Collided = true;

            if (moveAmount.Y > 0)
            {
                location = new Vector2(location.X, actorLocation.Y - this.collideHeight - 1);
            }
            else if (moveAmount.Y < 0)
            {
                location = new Vector2(location.X, actorLocation.Y + actorManager.GetActorHeight(corner, this));
            }

            moveAmount.Y = 0;
            velocity.Y = 0;
        }

        void HorizontalCollision(Vector2 actorLocation, Vector2 corner, ref Vector2 moveAmount)
        {
            Collided = true;

            if (moveAmount.X > 0)
            {
                location = new Vector2(actorLocation.X - this.collideWidth - 1, location.Y);
            }
            else if (moveAmount.X < 0)
            {
                location = new Vector2(actorLocation.X + actorManager.GetActorWidth(corner, this), location.Y);
            }

            moveAmount.X = 0;
            velocity.X = 0;
        }

        #endregion

        #region Update

        public void UpdateInactive(GameTime gameTime)
        {
            HandleTransparency(gameTime);
            if (!IsActive)
            {
                ManipulateVector(ref velocity, 240.0f, 10f);

                base.Update(gameTime);

                AdjustLocationInMap();
            }
        }

        public override void Update(GameTime gameTime)
        {
            movementScript.RotationState = camera.RotationState;
            Interaction = false;

            if (movementScript.MoveUp)
            {
                velocity += new Vector2(0, -step);
                movementState = 0;
            }
            else if (movementScript.MoveDown)
            {
                velocity += new Vector2(0, +step);
                movementState = 1;
            }
            if (movementScript.MoveLeft)
            {
                velocity += new Vector2(-step, 0);
                movementState = 2;
            }
            else if (movementScript.MoveRight)
            {
                velocity += new Vector2(step, 0);
                movementState = 3;
            }

            //TODO: get input from input configuration
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(Keys.Space))
            {
                Interaction = true;
                ToggleGate();
            }

            actorManager.IntersectsWithCoin(this.CollisionRectangle);
            actorManager.InteractsWithHiddenWall(this.CollisionRectangle, this);
            actorManager.IntersectsWithCloneBox(this.CollisionRectangle, this);

            ManipulateVector(ref velocity, 240.0f, 10f);

            base.Update(gameTime);

            AdjustLocationInMap();

        }

        #endregion

    }
}
