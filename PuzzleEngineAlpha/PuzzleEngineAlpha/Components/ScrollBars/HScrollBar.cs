using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.ScrollBars
{
    using Input;

    public class HScrollBar : IScrollBar
    {
        #region Declarations

        private readonly Camera.Camera camera;
        private Vector2 BarLocation;
        private Vector2 bulletLocation;
        private Texture2D bulletTexture;
        private Texture2D barTexture;
        private Vector2 size;
        private float barHeight;

        #endregion

        #region Constructor

        public HScrollBar(Texture2D bulletTexture, Texture2D barTexture, Camera.Camera camera, Vector2 location, Vector2 size, float barHeight)
        {
            this.BarLocation = location;
            this.size = size;
            this.camera = camera;
            this.bulletLocation = location;
            this.bulletTexture = bulletTexture;
            this.barTexture = barTexture;
            this.barHeight = barHeight;
            IsDragging = IsLocked = false;
        }

        #endregion

        #region Properties

        public Camera.Camera Camera
        {
            get
            {
                return camera;
            }
        }

        private Vector2 BulletLocation
        {
            get
            {
                return bulletLocation;
            }
            set
            {
                float moveAmount = MathHelper.Clamp(value.X, BarLocation.X, BarLocation.X + size.X - BulletSize.X);
                camera.Move(new Vector2(((moveAmount - bulletLocation.X) * Step),0));
                bulletLocation.X = moveAmount;
            }
        }

        private float Step
        {
            get
            {
                return camera.WorldRectangle.Width / camera.ViewPortWidth;
            }
        }

        private Rectangle BulletRectangle
        {
            get
            {
                return new Rectangle((int)bulletLocation.X, (int)bulletLocation.Y, (int)BulletSize.X, (int)BulletSize.Y);
            }
        }

        private Rectangle BarRectangle
        {
            get
            {
                return new Rectangle((int)BarLocation.X, (int)BarLocation.Y, (int)size.X, (int)barHeight);
            }
        }
        private bool IsLocked
        {
            get;
            set;
        }

        private bool IsDragging
        {
            get;
            set;
        }

        private Vector2 OffSet
        {
            get;
            set;
        }

        public Vector2 BulletSize
        {
            get
            {
                return new Vector2(MathHelper.Max(size.X - (camera.WorldRectangle.Width - camera.ViewPortWidth), barHeight / 2),barHeight);
            }
        }

        public bool Show
        {
            get
            {
                return (camera.WorldRectangle.Width > camera.ViewPortWidth);
            }
        }

        #endregion

        #region Drag

        private void Drag()
        {
            float locToTest = BulletLocation.X - OffSet.X;
            if (InputHandler.MousePosition.X > locToTest)
                BulletLocation += InputHandler.MousePosition - new Vector2(locToTest, 0);
            else if (InputHandler.MousePosition.X < locToTest)
                BulletLocation += InputHandler.MousePosition - new Vector2(locToTest, 0);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (InputHandler.LeftButtonIsClicked())
            {
                if (InputHandler.MouseRectangle.Intersects(BulletRectangle) && !IsLocked)
                {
                    if (!IsDragging)
                        OffSet = BulletLocation - InputHandler.MousePosition;
                    IsDragging = true;
                }
                else
                    IsLocked = true;
            }
            else
            {
                IsLocked = IsDragging = false;
            }

            if (IsDragging)
            {
                Drag();
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Show)
            {
                spriteBatch.Draw(bulletTexture, BulletRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                spriteBatch.Draw(barTexture, BarRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            }
        }

        #endregion
    }
}
