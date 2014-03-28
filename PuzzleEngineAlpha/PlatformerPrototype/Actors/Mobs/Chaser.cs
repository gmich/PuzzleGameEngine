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
        Vector2 currentTargetSquare;

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
            AI = new AStar(this.TileMap);
        }

        #endregion

        #region Properties

        public AI AI
        {
            get;
            set;
        }

        Vector2 Direction
        {
            get;
            set;
        }

        Vector2 Target
        {
            get;
            set;
        }

        #endregion

        #region AI Methods

        Vector2 DetermineMoveDirection()
        {
            if (ReachedTargetSquare())
            {
                currentTargetSquare = GetNewTargetSquare();
            }

            Vector2 squareCenter = TileMap.GetCellCenter(currentTargetSquare);

            return squareCenter - WorldCenter;
        }

        bool ReachedTargetSquare()
        {
            return (
                Vector2.Distance(WorldCenter, TileMap.GetCellCenter(currentTargetSquare)) <= 2);
        }

        Vector2 GetNewTargetSquare()
        {
            List<Vector2> path = AI.FindPath(TileMap.GetCellByPixel(WorldCenter), TileMap.GetCellByPixel(Target));

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

        #region Update And Movement

        public override void Move()
        {
            return;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion

    }
}
