using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.ScrollBars
{
    using Input;

    public class VScrollBar
    {
        #region Declarations

        private readonly Camera.Camera camera;
        public Vector2 bulletLocation;
        private Texture2D bulletTexture;
        private Texture2D barTexture;
        private Vector2 size;
        private float layer;

        #endregion

        #region Constructor

        public VScrollBar(Texture2D bulletTexture, Texture2D barTexture, Camera.Camera camera, Vector2 location, Vector2 size,float layer)
        {
            this.BarLocation = location;
            this.size = size;
            this.camera = camera;
            this.bulletLocation = location;
            this.bulletTexture = bulletTexture;
            this.barTexture = barTexture;
            IsDragging = IsLocked = false;
            this.layer = layer;
        }

        #endregion

        #region Properties

        public Vector2 BarLocation
        {
            get;
            set;
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public Camera.Camera Camera
        {
            get
            {
                return camera;
            }
        }

        public Vector2 BulletLocation
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

        private Rectangle AreaRectangle
        {
            get
            {
                return new Rectangle((int)BarLocation.X - (int)Camera.ViewPortWidth, (int)BarLocation.Y, (int)Camera.ViewPortWidth, (int)Camera.ViewPortHeight);
            }
        }
        private Rectangle BarRectangle
        {
            get
            {
                return new Rectangle((int)BarLocation.X, (int)BarLocation.Y, (int)size.X, (int)size.Y);
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
                return new Vector2(size.X, MathHelper.Max(size.Y - (camera.WorldRectangle.Height - camera.ViewPortHeight), size.X/ 2));
            }
        }

        public bool Show
        {
            get
            {
                if (camera.WorldRectangle.Height > camera.ViewPortHeight)
                {
                    return true;
                }

                camera.Position = new Vector2(0, 0);
                bulletLocation = new Vector2(bulletLocation.X, BarLocation.Y);
                return false;
            }
        }

        #endregion

        #region Public Helper Methods

        public void MoveUp()
        {
            BulletLocation += new Vector2(0,-10);
        }

        public void MoveDown()
        {
            BulletLocation += new Vector2(0, 10);
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
            if (AreaRectangle.Intersects(InputHandler.MouseRectangle))
            {
                if (InputHandler.IsWheelMovingDown())
                    MoveDown();
                if (InputHandler.IsWheelMovingUp())
                    MoveUp();
            }

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
                spriteBatch.Draw(barTexture, BarRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, layer + 0.01f);
                spriteBatch.Draw(bulletTexture, BulletRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None,layer);
            }
        }

        #endregion
    }
}
