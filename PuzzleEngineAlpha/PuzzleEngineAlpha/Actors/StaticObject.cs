using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors
{
    using Animations;

    public class StaticObject
    {
        #region Declarations

        protected Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
        protected Vector2 location;
        protected string currentAnimation;
        protected readonly Level.TileMap tileMap;
        protected readonly Camera.Camera camera;
        protected bool enabled;
        int frameWidth;
        int frameHeight;

        #endregion

        #region Constuctor

        public StaticObject(Level.TileMap tileMap, Camera.Camera camera, Vector2 location, int frameWidth, int frameHeight)
        {
            this.tileMap = tileMap;
            this.camera = camera;
            this.location = location;
            this.enabled = true;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.Layer = 0.0f;
            this.Transparency = 1.0f;
        }

        #endregion

        #region Rendering Properties

        
        protected float Layer
        {
            get;
            set;
        }

        protected float Transparency
        {
            get;
            set;
        }

        public Rectangle CollisionRectangle
        {
            get;
            set;
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle((int)location.X, (int)location.Y, frameWidth, frameHeight);
            }
        }

        #endregion

        #region Helper Methods

        public bool Intersects(Vector2 otherLocation)
        {
            if (!enabled) return false;
            return ((location.X <= otherLocation.X && location.Y < otherLocation.Y)
                    && (location.X + CollisionRectangle.Width > otherLocation.X && location.Y + CollisionRectangle.Height >= otherLocation.Y));
        }

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

        #region Public Methods

        public void PlayAnimation(string name)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }

        #endregion

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateAnimation(gameTime);

        }

        #endregion

        #region Draw

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                spriteBatch.Draw(animations[currentAnimation].Texture, camera.WorldToScreen(WorldRectangle), animations[currentAnimation].FrameRectangle, Color.White * Transparency, 0.0f, Vector2.Zero, SpriteEffects.None,Layer);
            }
        }

        #endregion
    }
}