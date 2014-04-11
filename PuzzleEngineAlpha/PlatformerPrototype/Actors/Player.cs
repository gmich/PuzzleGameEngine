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
        readonly Handlers.ActorManager actorManager;
        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;
        int movementState;

        #endregion

        #region Constructor

        public Player(Handlers.ActorManager actorManager,PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, float step, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
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
            movementState = 0;
            applyJumpForce = false;
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

        public Handlers.WeaponManager WeaponManager
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
        readonly Vector2 MaxJump = new Vector2(0, -400);
        readonly Vector2 InitialJump = new Vector2(0, -150);
        Vector2 jumpForce = new Vector2(0, -40);
        bool applyJumpForce;

        bool CanJump
        {
            get
            {
                return (OnGround);
            }
        }

        void Jump()
        {
            if (applyJumpForce && movementScript.MoveUp)
            {
                this.velocity.Y = MathHelper.Max(velocity.Y + jumpForce.Y, MaxJump.Y);
            }
            if (velocity.Y == MaxJump.Y || velocity.Y >= 0)
            {
                applyJumpForce = false;
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
          /*  if (actorManager.HasActorAtLocation(corner1-moveAmount,this) || actorManager.HasActorAtLocation(corner2-moveAmount,this))
            {
                moveAmount.X = 0;
                velocity.X = 0;
                Collided = true;
                return;
            }*/

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
         /*   if (actorManager.HasActorAtLocation(corner1 - moveAmount, this) || actorManager.HasActorAtLocation(corner2 - moveAmount, this))
            {
                moveAmount.Y = 0;
                velocity.Y = 0;
                Collided = true;
                return;
            }*/

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
                location = new Vector2(location.X,(float)Math.Floor(actorLocation.Y - this.collideHeight-1));
                OnGround = true;
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

        #region Weapon Related Methods

        Vector2 WeaponVelocity
        {
            get
            {

            if (movementState == 0)
                return new Vector2(-1, 0);
            else
                return new Vector2(1, 0);
            }
        }

        Vector2 WeaponLocation
        {
            get
            {
                float offSet = this.collideWidth/2 + 8;

                if (movementState == 0)
                    return new Vector2(this.location.X - offSet, this.location.Y);
                else
                    return new Vector2(this.location.X + this.collideWidth + offSet, this.location.Y);
            }
        }

        #endregion

        #region Update

        public override void Move()
        {
            if (!IsActive) return;

            if (movementScript.MoveUp)
            {
                if (CanJump)
                {
                    velocity += InitialJump;
                    applyJumpForce = true;
                }
            }
            else
            {
                applyJumpForce = false;
            }
            Jump();

            if (movementScript.MoveLeft)
            {
                velocity += new Vector2(-step, 0);
                movementState = 0;
            }
            else if (movementScript.MoveRight)
            {
                velocity += new Vector2(step, 0);
                movementState = 1;
            }

            //TODO: get input from input configuration
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(Keys.LeftControl))
            {
                Interaction = true;
                ToggleGate();
            }
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(Keys.Space))
            {
                WeaponManager.Shoot(WeaponLocation, WeaponVelocity);
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

            ManipulateVector(ref velocity, 320.0f, 10f);

            base.Update(gameTime);

            if (VCollided) applyJumpForce = false;

            velocity += Gravity;
            AdjustLocationInMap();
            AdjustCamera();
          
        }

        #endregion

    }
}
