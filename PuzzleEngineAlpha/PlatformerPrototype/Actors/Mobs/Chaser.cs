using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;
using PuzzleEngineAlpha.Level;

namespace PlatformerPrototype.Actors.Mobs
{
    using Animations;
    using AI;

    public class Chaser : MapObject
    {
        #region Declarations

        readonly Handlers.ActorManager actorManager;
        readonly ParticleManager particleManager;
        readonly TileMap TileMap;
        readonly Vector2 Gravity = new Vector2(0, 15);
        readonly Vector2 Jump = new Vector2(0, -430);
        Vector2 currentTargetSquare;
        float step;

        #endregion

        #region Constructor

        public Chaser(Handlers.ActorManager actorManager, ParticleManager particleManager, PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, ContentManager content, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap, camera, location, frameWidth, frameHeight, collideWidth, collideHeight)
        {
            this.actorManager = actorManager;
            this.particleManager = particleManager;
            this.TileMap = tileMap;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(content.Load<Texture2D>(@"Textures/Mobs/chaser"), frameWidth, "run"));
            currentAnimation = "run";
            this.actorManager = actorManager;
            AI = new AStarPlatformer(this.TileMap);
            timeSinceTargetSquare = 0.0f;
            Lives = 2;
            step = 25.0f;
        }

        #endregion

        #region Properties

        public AI AI
        {
            get;
            set;
        }

        public MapObject ChaseTarget
        {
            get;
            set;
        }

        Rectangle TargetSquareRectangle
        {
            get
            {
                return new Rectangle((int)currentTargetSquare.X, (int)currentTargetSquare.Y, this.tileMap.TileWidth, this.tileMap.TileHeight);
            }
        }

        Vector2 Target
        {
            get
            {
                return ChaseTarget.location + new Vector2(ChaseTarget.collideWidth ,ChaseTarget.collideHeight);
            }
        }

        #endregion

        #region Helper Methods

        float Step(float timePassed)
        {
            step = MathHelper.Min(25.0f, step + timePassed * 3);
            return step;
        }

        void ManipulateVector(ref Vector2 vector, float maxAcceleration, float amount)
        {
            if (vector.X > 0)
                vector.X = MathHelper.Clamp(vector.X - amount, 0, maxAcceleration);
            else
                vector.X = MathHelper.Clamp(vector.X + amount, -maxAcceleration, 0);
        }

        #endregion

        #region AI Methods

        float timeSinceTargetSquare;
        const float timeToGetNewTargetSquare = 0.5f;
        Vector2 DetermineMoveDirection()
        {
            if (ReachedTargetSquare())
            {
                currentTargetSquare = GetNewTargetSquare();
                timeSinceTargetSquare = 0.0f;
            }
            else if (timeSinceTargetSquare > timeToGetNewTargetSquare)
            {
                currentTargetSquare = GetNewTargetSquare();
                timeSinceTargetSquare = 0.0f;
            }

            Vector2 squareCenter = TileMap.GetCellCenter(currentTargetSquare);

            return squareCenter - WorldCenter + new Vector2(0, this.frameHeight+1);
        }

        bool ReachedTargetSquare()
        {
            return (Vector2.Distance(WorldCenter, TileMap.GetCellCenter(currentTargetSquare)) <= tileMap.TileHeight);
        }

        Vector2 GetNewTargetSquare()
        {
            List<Vector2> path = AI.FindPath(TileMap.GetCellByPixel(WorldCenter), TileMap.GetCellByPixel(Target));

            if (path == null) return TileMap.GetCellByPixel(Target);

            if (path.Count > 1)
            {
                return new Vector2(path[1].X, path[1].Y);
            }
            else
            {
                return TileMap.GetCellByPixel(Target);
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

        #region Bullet Related Methods and Properties

        int Lives
        {
            get;
            set;
        }

        const float Mass = 5.0f;

        Vector2 GetBulletCollisionVelocity
        {
            get
            {
                return actorManager.GetBulletCollisionVelocity(this.CollisionRectangle);
            }
        }

        void GotHit(Vector2 impactVelocity)
        {
            Lives--;
            this.velocity += impactVelocity * Mass;
            step = 15.0f;
        }

        void CheckBulletCollision()
        {
            Vector2 BulletVelocity = this.GetBulletCollisionVelocity;

            if (BulletVelocity != Vector2.Zero)
            {
                this.GotHit(BulletVelocity);
            }

            if (Lives == 0)
            {
                AddDestructionParticles();
                this.Expired = true;
            }
            
        }

        void AddDestructionParticles()
        {
            particleManager.AddRectangleDestructionParticles(new Color(239,228,176),this.location, this.collideWidth, this.collideHeight, 2, 2);
        }

        #endregion

        #region Update And Movement

        public override void Move()
        {
            return;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = DetermineMoveDirection();
            direction.Normalize();

            float timePassed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (direction.Y < 0 && OnGround)
            {
                this.velocity += Jump;
            }
            if (direction.X > 0)
            {
                velocity.X += Step(timePassed);
            }
            if (direction.X < 0)
            {
                velocity.X -= Step(timePassed);
            }
            velocity += Gravity;

            ManipulateVector(ref velocity, 285.0f, 10f);

            timeSinceTargetSquare += timePassed;
            
            base.Update(gameTime);

            AdjustLocationInMap();
            CheckBulletCollision();


        }

        #endregion

    }
}
