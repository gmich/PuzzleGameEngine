using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;

namespace PlatformerPrototype.Actors
{
    using Animations;

    public class PlayerClone : MapObject
    {

        #region Declarations

        Queue<Vector2> Velocities;
        Queue<bool> interactions;
        const int queueLimit = 200;
        readonly ActorManager actorManager;
        readonly ParticleManager particleManager;

        #endregion

        #region Constructor

        public PlayerClone(ActorManager actorManager, ParticleManager particleManager, PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, ContentManager content, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap, camera, location, frameWidth, frameHeight, collideWidth, collideHeight)
        {
            this.actorManager = actorManager;
            this.particleManager = particleManager;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(content.Load<Texture2D>(@"Textures/player"), frameWidth, "run"));
            currentAnimation = "run";
            this.actorManager = actorManager;

            Reset();
        }

        #endregion

        #region Clone Properties

        bool HaveToRecord
        {
            get
            {
                return (Velocities.Count < queueLimit);
            }
        }

        public bool IsAlive
        {
            get;
            set;
        }

        public bool Destroy
        {
            get;
            set;
        }

        public Rectangle InteractionRectangle
        {
            get;
            set;
        }

        public Player playerToRecord
        {
            get;
            set;
        }

        #endregion

        #region Helper Methods

        void Reset()
        {
            Velocities = new Queue<Vector2>();
            interactions = new Queue<bool>();
            IsAlive = false;
            Destroy = false;
            enabled = false;
        }

        void Interact()
        {
            Button button = actorManager.GetInteractionButton(this.CollisionRectangle);

            if (button != null)
            {
                actorManager.ToggleGatesWithTag(button.Tag);
            }
        }
        #endregion

        #region Collision Detection

        public override void HorizontalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1 - moveAmount, this) || actorManager.HasActorAtLocation(corner2 - moveAmount, this))
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

            if (actorManager.HasActorAtLocation(corner1, this))
            {
                VerticalCollision(actorManager.GetActorLocation(corner1, this), corner1, ref moveAmount);
            }
            if (actorManager.HasActorAtLocation(corner2, this))
            {
                VerticalCollision(actorManager.GetActorLocation(corner2, this), corner2, ref moveAmount);
            }
        }

        void VerticalCollision(Vector2 actorLocation, Vector2 corner, ref Vector2 moveAmount)
        {
            Collided = true;

            if (moveAmount.Y > 0)
                location = new Vector2(location.X, actorLocation.Y - this.collideHeight - 1);
            else if (moveAmount.Y < 0)
                location = new Vector2(location.X, actorLocation.Y + actorManager.GetActorHeight(corner, this));

            moveAmount.Y = 0;
            velocity.Y = 0;
        }

        void HorizontalCollision(Vector2 actorLocation, Vector2 corner, ref Vector2 moveAmount)
        {
            Collided = true;

            if (moveAmount.X > 0)
                location = new Vector2(actorLocation.X - this.collideWidth - 1, location.Y);
            else if (moveAmount.X < 0)
                location = new Vector2(actorLocation.X + actorManager.GetActorWidth(corner, this), location.Y);

            moveAmount.X = 0;
            velocity.X = 0;
        }

        void AdjustLocationInMap()
        {
            this.location.X = MathHelper.Clamp(this.location.X, 0, camera.WorldSize.X);
            this.location.Y = MathHelper.Clamp(this.location.Y, 0, camera.WorldSize.Y);
        }

        #endregion

        public override void Move()
        {
            return;
        }

        public override void Update(GameTime gameTime)
        {
            if (playerToRecord.CollisionRectangle.Intersects(this.InteractionRectangle))
            {
                if(IsAlive)
                    particleManager.AddRecordingParticles(this.WorldCenter, (queueLimit), 2, 2, 80);

                Reset();
                location = playerToRecord.location;
                particleManager.AddRecordingParticles(playerToRecord.Center, (queueLimit - Velocities.Count)/2, 1, 1,25);
                return;
            }
            else if (!(playerToRecord.CollisionRectangle.Intersects(this.InteractionRectangle)) && HaveToRecord && !IsAlive)
            {
                // if (this.location == Vector2.Zero)
                // location = playerToRecord.location;
                particleManager.AddRecordingParticles(playerToRecord.Center, (queueLimit - Velocities.Count)/2, 1, 1, 25);

                Velocities.Enqueue(playerToRecord.Velocity);
                interactions.Enqueue(playerToRecord.Interaction);

                if (!HaveToRecord)
                {
                    enabled = true;
                    IsAlive = true;
                }
            }
            else if (IsAlive)
            {
                if (Velocities.Count > 0)
                {
                    this.Velocity = Velocities.Dequeue();
                    particleManager.AddRecordingParticles(this.WorldCenter, (Velocities.Count)/2, 1, 1, 25);
                    if (interactions.Dequeue())
                        Interact();
                }
                else
                {
                    particleManager.AddRecordingParticles(this.WorldCenter, (queueLimit), 2, 2, 70);
                   // particleManager.AddRectangleDestructionParticles(this.location, this.frameWidth, this.frameHeight, 1, 1);
                    Destroy = true;
                }

                actorManager.IntersectsWithCoin(this.CollisionRectangle);
                actorManager.InteractsWithHiddenWall(this.CollisionRectangle, this);

                base.Update(gameTime);

                AdjustLocationInMap();

                //TODO: may not be necessary
                // if (Collided)
                //  Destroy = true;
            }
        }

    }
}
