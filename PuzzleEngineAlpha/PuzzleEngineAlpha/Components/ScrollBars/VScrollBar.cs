using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.ScrollBars
{
    using Input;

    public class VScrollBar : IScrollBar
    {
        #region Declarations

        private readonly Camera.Camera camera;
        private Vector2 BarLocation;
        private Vector2 bulletLocation;
        private Texture2D bulletTexture;
        private Texture2D barTexture;
        private Vector2 size;
        private float barWidth;

        #endregion

        #region Constructor

        public VScrollBar(Texture2D bulletTexture, Texture2D barTexture, Camera.Camera camera, Vector2 location, Vector2 size, float barWidth)
        {
            this.BarLocation = location;
            this.size = size;
            this.camera = camera;
            this.bulletLocation = location;
            this.bulletTexture = bulletTexture;
            this.barTexture = barTexture;
            this.barWidth = barWidth;
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
                float moveAmount = MathHelper.Clamp(value.Y, BarLocation.Y, BarLocation.Y + size.Y - BulletSize.Y);
                camera.Move(new Vector2(0, ((moveAmount - bulletLocation.Y) * Step)));
                bulletLocation.Y = moveAmount;
            }
        }

        private float Step
        {
            get
            {
                return camera.WorldRectangle.Height / camera.ViewPortHeight;
            }
        }

        private Rectangle BulletRectangle
        {
            get
            {
                return new Rectangle((int)bulletLocation.X, (int)bulletLocation.Y, (int)BulletSize.X,(int)BulletSize.Y);
            }
        }

        private Rectangle BarRectangle
        {
            get
            {
                return new Rectangle((int)BarLocation.X, (int)BarLocation.Y, (int)barWidth, (int)size.Y);
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
                return new Vector2(barWidth, MathHelper.Max(size.Y - (camera.WorldRectangle.Height - camera.ViewPortHeight), barWidth/2));
            }
        }
        public bool Show
        {
            get
            {
                return (camera.WorldRectangle.Height > camera.ViewPortHeight);
            }
        }

        #endregion

        #region Public Helper Methods

        public void MoveUp()
        {
            BulletLocation += new Vector2(0,-8);
        }

        public void MoveDown()
        {
            BulletLocation += new Vector2(0, 8);
        }

        #endregion

        #region Drag

        private void Drag()
        {
            float locToTest = BulletLocation.Y - OffSet.Y;
            if( InputHandler.MousePosition.Y >  locToTest)
                BulletLocation += InputHandler.MousePosition - new Vector2(0, locToTest);
            else if (InputHandler.MousePosition.Y < locToTest)
                BulletLocation += InputHandler.MousePosition - new Vector2(0, locToTest);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (InputHandler.LeftButtonIsClicked())
            {
                if (InputHandler.MouseRectangle.Intersects(BulletRectangle) && !IsLocked)
                {
                    if(!IsDragging)
                        OffSet = BulletLocation - InputHandler.MousePosition;
                    IsDragging = true;
                }
                else
                    IsLocked = true;
            }
            else
            {
                IsLocked=IsDragging=false;
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
