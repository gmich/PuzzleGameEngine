using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerPrototype.Actors
{
    using PuzzleEngineAlpha.Input.Scripts;
    using PuzzleEngineAlpha.Scene;

    public class Player : PuzzleEngineAlpha.Actors.MapObject
    {

        #region Declarations

        MovementScript movementScript;
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

        #region Physics

        readonly Vector2 Gravity = new Vector2(0, 15);
        readonly Vector2 Jump = new Vector2(0, -450);

        bool CanJump
        {
            get
            {
                return (VCollided && velocity.Y >= 0);
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

            if (velocity.Y <= 30 && velocity.Y >= 0)
                return new Vector2(velocity.X, 0);
            else
                return new Vector2(velocity.X, velocity.Y/2);
        }

        void ManipulateVector(ref Vector2 vector, float maxAcceleration,float amount)
        {
            if (vector.X > 0)
                vector.X = MathHelper.Clamp(vector.X - amount, 0, maxAcceleration);
            else
                vector.X = MathHelper.Clamp(vector.X + amount, -maxAcceleration, 0);
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

        void AdjustCamera()
        {
            camera.Position = new Vector2((int)camera.Position.X,(int)camera.Position.Y);
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
            VCollided = true;

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
            HCollided = true;

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

        public override void Move()
        {
            if (!IsActive) return;

            if (movementScript.MoveUp && CanJump)
            {
                velocity += Jump;
            }

            if (movementScript.MoveLeft)
            {
                velocity += new Vector2(-step, 0);
            }
            else if (movementScript.MoveRight)
            {
                velocity += new Vector2(step, 0);
            }
            //TODO: get input from input configuration
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(Keys.Space))
            {
                Interaction = true;
                ToggleGate();
            }
        }

        public override void Update(GameTime gameTime)
        {
            HandleTransparency(gameTime);
            movementScript.RotationState = camera.RotationState;
            Interaction = false;

            actorManager.IntersectsWithCoin(this.CollisionRectangle);
            actorManager.InteractsWithHiddenWall(this.CollisionRectangle, this);
            actorManager.IntersectsWithCloneBox(this.CollisionRectangle, this);

            ManipulateVector(ref velocity, 300.0f, 10f);

            base.Update(gameTime);
            velocity += Gravity;
            AdjustLocationInMap();
            AdjustCamera();
        }

        #endregion

    }
}
