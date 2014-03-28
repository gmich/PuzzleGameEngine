using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;

namespace PlatformerPrototype.Actors.Weapons
{
    using Animations;
    using Actors.Handlers;

    public class Bullet : MapObject
    {

        #region Declarations

        readonly Vector2 memento;
        readonly ActorManager actorManager;
        readonly ParticleManager particleManager;

        #endregion

        #region Constructor

        public Bullet(ActorManager actorManager, ParticleManager particleManager, PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Vector2 velocity, Vector2 memento,ContentManager content, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap, camera, location, frameWidth, frameHeight, collideWidth, collideHeight)
        {
            this.velocity = velocity;
            this.memento = memento;
            this.actorManager = actorManager;
            this.particleManager = particleManager;
            this.animations.Add("bullet", new PuzzleEngineAlpha.Animations.AnimationStrip(content.Load<Texture2D>(@"Textures/Guns/bullet"), frameWidth, "bullet"));
            currentAnimation = "bullet";
            this.actorManager = actorManager;
            this.Destroy = false;
            this.enabled = true;
        }

        #endregion

        #region Properties

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

        #endregion

        #region Collision Detection

        public override void HorizontalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1 - moveAmount) || actorManager.HasActorAtLocation(corner2 - moveAmount))
            {
                moveAmount.X = 0;
                velocity.X = 0;
                Collided = true;
                return;
            }

            if (actorManager.HasActorAtLocation(corner1))
            {
                HorizontalCollision(actorManager.GetActorLocation(corner1), corner1, ref moveAmount);
            }
            if (actorManager.HasActorAtLocation(corner2))
            {
                HorizontalCollision(actorManager.GetActorLocation(corner2), corner2, ref moveAmount);
            }
        }

        public override void VerticalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1 - moveAmount) || actorManager.HasActorAtLocation(corner2 - moveAmount))
            {
                moveAmount.Y = 0;
                velocity.Y = 0;
                Collided = true;
                return;
            }

            if (actorManager.HasActorAtLocation(corner1))
            {
                VerticalCollision(actorManager.GetActorLocation(corner1), corner1, ref moveAmount);
            }
            if (actorManager.HasActorAtLocation(corner2))
            {
                VerticalCollision(actorManager.GetActorLocation(corner2), corner2, ref moveAmount);
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
                location = new Vector2(location.X, actorLocation.Y + actorManager.GetActorHeight(corner));
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
                location = new Vector2(actorLocation.X + actorManager.GetActorWidth(corner), location.Y);
            }

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

        #region Update

        public override void Update(GameTime gameTime)
        {
            velocity += memento;
            base.Update(gameTime);

            if (Collided) 
                this.Destroy = true;
        }

        #endregion

    }

}

