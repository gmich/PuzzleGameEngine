using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors
{
    using Animations;

    public class MapObject
    {
        #region Declarations

        public Vector2 location;
        protected Vector2 velocity;
        protected int frameWidth;
        protected int frameHeight;
        protected bool enabled;
        protected Rectangle collisionRectangle;
        protected int collideWidth;
        protected int collideHeight;
        protected bool codeBasedBlocks = true;
        protected float drawDepth;
        protected Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;
        protected readonly Level.TileMap tileMap;
        protected readonly Camera.Camera camera;
    
        #endregion

        #region Constructor

        public MapObject(Level.TileMap tileMap,Camera.Camera camera, Vector2 worldLocation, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
        {
            this.enabled = true;
            this.location = worldLocation;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
            this.collideHeight = collideHeight;
            this.collideWidth = collideWidth;
            this.tileMap = tileMap;
            this.camera = camera;
        }

        #endregion

        #region Properties

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
                return new Rectangle(
                    (int)location.X,
                    (int)location.Y,
                    frameWidth,
                    frameHeight);
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

        #region Map-Based Collision Detection Methods

        private Vector2 HorizontalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.X == 0)
                return moveAmount;

            Rectangle afterMoveRect = CollisionRectangle;

            afterMoveRect.Offset((int)moveAmount.X, 0);

            Vector2 corner1, corner2;

            if (moveAmount.X < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left, afterMoveRect.Top );
                corner2 = new Vector2(afterMoveRect.Left, afterMoveRect.Bottom );
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Right, afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right, afterMoveRect.Bottom);
            }

            Vector2 mapCell1 = tileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = tileMap.GetCellByPixel(corner2);

            if (!tileMap.CellIsPassable(mapCell1) ||
                !tileMap.CellIsPassable(mapCell2))
            {
                Collided = true;
                moveAmount.X = 0;
                velocity.X = 0;
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

            Rectangle afterMoveRect = CollisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, (int)moveAmount.Y);
            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left, afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right, afterMoveRect.Top);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Left, afterMoveRect.Bottom);
                corner2 = new Vector2(afterMoveRect.Right, afterMoveRect.Bottom);
            }

            Vector2 mapCell1 = tileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = tileMap.GetCellByPixel(corner2);

            if (!tileMap.CellIsPassable(mapCell1) || !tileMap.CellIsPassable(mapCell2))
            {
                Collided = true;
                moveAmount.Y = 0;
                velocity.Y = 0;
            }
            
            if (codeBasedBlocks)
            {
                if (tileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    tileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    Collided = true;
                    moveAmount.Y = 0;
                    velocity.Y = 0;
                }
            }

            return moveAmount;
        }

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
                spriteBatch.Draw(animations[currentAnimation].Texture, camera.WorldToScreen(WorldRectangle), animations[currentAnimation].FrameRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, drawDepth);
            }
        }
    
        #endregion

    }
}
