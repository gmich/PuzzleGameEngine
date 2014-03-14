using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors
{
    using Animations;

    public abstract class MapObject
    {
        #region Declarations

        public Vector2 location;
        protected Vector2 velocity;
        public int frameWidth;
        public int frameHeight;
        protected bool enabled;
        protected Rectangle collisionRectangle;
        public int collideWidth;
        public int collideHeight;
        protected bool codeBasedBlocks = true;
        protected float drawDepth;
        protected Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;
        protected readonly Level.TileMap tileMap;
        protected readonly Camera.Camera camera;
    
        #endregion

        #region Constructor

        public MapObject(Level.TileMap tileMap,Camera.Camera camera, Vector2 location, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
        {
            this.enabled = true;
            this.location = location;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
            this.collideHeight = collideHeight;
            this.collideWidth = collideWidth;
            this.tileMap = tileMap;
            this.camera = camera;
            this.Transparency = 1.0f;
        }

        #endregion

        #region Properties

        protected float Transparency
        {
            get;
            set;
        }

        public bool Collided
        {
            get;
            set;
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public string CurrentAnimation
        {
            set { currentAnimation = value ; }
        }

        public Vector2 WorldLocation
        {
            get { return location; }
            set { location = value; }
        }

        public Vector2 Velocity
        {
            set { velocity = value; }
            get { return velocity; }
        }

        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                  (int)location.X + (int)(frameWidth / 2),
                  (int)location.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);
            }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)location.X, (int)location.Y, collideWidth, collideHeight);
            }
        }


        #endregion 

        #region Helper Methods

        void UpdateAnimation(GameTime gameTime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }

        #endregion

        #region Intersection

        public bool Intersects(Vector2 otherLocation)
        {
            if (!enabled) return false;
            return ((CollisionRectangle.X <= otherLocation.X && CollisionRectangle.Y < otherLocation.Y)
                    && (CollisionRectangle.X + CollisionRectangle.Width + 1> otherLocation.X && CollisionRectangle.Y + CollisionRectangle.Height+1 >= otherLocation.Y));
        }

        #endregion

        #region Map-Based Collision Detection Methods

        private Vector2 HorizontalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.X == 0)
                return moveAmount;

            Vector2 corner1, corner2;

            if (moveAmount.X < 0)
            {
                corner1 = new Vector2(location.X + moveAmount.X, location.Y);
                corner2 = new Vector2(location.X + moveAmount.X, location.Y+collideHeight);
            }
            else
            {
                corner1 = new Vector2(location.X + moveAmount.X + collideWidth, location.Y);
                corner2 = new Vector2(location.X + moveAmount.X + collideWidth, location.Y + collideHeight);
            }
            HorizontalActorCollision(ref moveAmount,corner1, corner2);
            Vector2 mapCell1 = tileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = tileMap.GetCellByPixel(corner2);

            if (!tileMap.CellIsPassable(mapCell1))
            {
                HorizontalCollision(mapCell1, ref moveAmount);
            }
            if (!tileMap.CellIsPassable(mapCell2))
            {
                HorizontalCollision(mapCell2, ref moveAmount);
            }

            if (codeBasedBlocks)
            {
                if (tileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    tileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    Collided = true;
                    moveAmount.X = 0;
                    velocity.X = 0;
                }
            }

            return moveAmount;
        }

        private Vector2 VerticalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.Y == 0)
                return moveAmount;

            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(location.X,location.Y+moveAmount.Y);
                corner2 = new Vector2(location.X + collideWidth, location.Y + moveAmount.Y);
            }
            else
            {
                corner1 = new Vector2(location.X, location.Y +collideHeight+ moveAmount.Y);
                corner2 = new Vector2(location.X + collideWidth, location.Y + collideHeight + moveAmount.Y);
            }

            Vector2 mapCell1 = tileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = tileMap.GetCellByPixel(corner2);
            VerticalActorCollision(ref moveAmount,corner1, corner2);

            if (!tileMap.CellIsPassable(mapCell1))
            {
                VerticalCollision(mapCell1, ref moveAmount);
            }
            if(!tileMap.CellIsPassable(mapCell2))
            {
                VerticalCollision(mapCell2, ref moveAmount);
            }

            if (codeBasedBlocks)
            {
                if (tileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    tileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    //TODO: call vertical collision function
                    Collided = true;
                    moveAmount.Y = 0;
                    velocity.Y = 0;
                }
            }

            return moveAmount;
        }

        void VerticalCollision(Vector2 mapCell, ref Vector2 moveAmount)
        {
            Collided = true;           

            if (moveAmount.Y > 0)
                location = new Vector2(location.X, tileMap.GetCellLocation(mapCell).Y - this.collideHeight-1);
            else if (moveAmount.Y < 0)
                location = new Vector2(location.X, tileMap.GetCellLocation(mapCell).Y + tileMap.TileHeight );

            moveAmount.Y = 0;
            velocity.Y = 0;
        }

        void HorizontalCollision(Vector2 mapCell, ref Vector2 moveAmount)
        {
            Collided = true;            

            if (moveAmount.X > 0)
                location = new Vector2(tileMap.GetCellLocation(mapCell).X - this.collideWidth - 1,location.Y);
            else if (moveAmount.X < 0)
                location = new Vector2(tileMap.GetCellLocation(mapCell).X + tileMap.TileWidth, location.Y );

            moveAmount.X = 0;
            velocity.X = 0;
        }

        public abstract void HorizontalActorCollision(ref Vector2 moveAmount,Vector2 corner1, Vector2 corner2);

        public abstract void VerticalActorCollision(ref Vector2 moveAmount,Vector2 corner1, Vector2 corner2);


        #endregion

        #region Public Methods

        public void PlayAnimation(string name)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            Collided = false;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateAnimation(gameTime);


            Vector2 moveAmount = velocity * elapsed;
      
            moveAmount = HorizontalCollisionTest(moveAmount);
            moveAmount = VerticalCollisionTest(moveAmount);

            Vector2 newPosition = location + moveAmount;

         /*   newPosition = new Vector2(
                MathHelper.Clamp(newPosition.X, 0,
                  camera.WorldRectangle.Width - frameWidth),
                MathHelper.Clamp(newPosition.Y, 2 * (-tileMap.TileHeight),
                  camera.WorldRectangle.Height - frameHeight));
            */
            location = newPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled)
                return;

            if (animations.ContainsKey(currentAnimation))
            {
                spriteBatch.Draw(animations[currentAnimation].Texture, camera.WorldToScreen(WorldRectangle), animations[currentAnimation].FrameRectangle, Color.White * Transparency, 0.0f, Vector2.Zero, SpriteEffects.None, drawDepth);
            }
        }
    
        #endregion

    }
}
