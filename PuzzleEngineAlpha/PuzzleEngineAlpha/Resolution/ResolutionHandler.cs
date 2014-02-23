using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Resolution
{
    using Input;

    public class ResolutionHandler
    {
        #region Declarations

        private GraphicsDeviceManager graphicsDeviceManager;
        private static int height;
        private int vheight;
        private static int width; 
        private int vwidth;
        private DisplayMode displayM;
        private bool isFullScreen;

        #endregion

        #region Constructor

        public ResolutionHandler(ref GraphicsDeviceManager device, bool newFullScreen)
        {
            width = device.PreferredBackBufferWidth;
            height = device.PreferredBackBufferHeight;
            this.graphicsDeviceManager = device;
            this.isFullScreen = newFullScreen;
            this.ApplyResolutionSettings();
        }

        #endregion

        #region Window Properties

        public static int WindowWidth
        {
            get
            {
                return width;
            }
        }

        public static int WindowHeight
        {
            get
            {
                return height;
            }
        }

        public static Rectangle ScreenRectangle
        {
            get
            {
                return new Rectangle(0, 0, WindowWidth, WindowHeight);
            }
        }

        #endregion

        #region Event

        public delegate void ChangedResolutionHandler(object sender, EventArgs e);
        public static event ChangedResolutionHandler Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        #endregion

        #region Methods

        public void SetResolution(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;
            this.ApplyResolutionSettings();
            OnChanged(EventArgs.Empty);
        }

        private void ApplyResolutionSettings()
        {
            if (this.isFullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    this.graphicsDeviceManager.PreferredBackBufferWidth = (int)MathHelper.Max(1, (float)width);
                    this.graphicsDeviceManager.PreferredBackBufferHeight = (int)MathHelper.Max(1, (float)height);
                    this.graphicsDeviceManager.IsFullScreen = this.isFullScreen;
                    this.graphicsDeviceManager.ApplyChanges();
                }
            }
            else
            {
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    if ((dm.Width == width) && (dm.Height == height))
                    {
                        this.graphicsDeviceManager.PreferredBackBufferWidth = width;
                        this.graphicsDeviceManager.PreferredBackBufferHeight = height;
                        this.graphicsDeviceManager.IsFullScreen = this.isFullScreen;
                        this.graphicsDeviceManager.ApplyChanges();
                        break;
                    }
                }
            }
            width = this.graphicsDeviceManager.PreferredBackBufferWidth;
            height = this.graphicsDeviceManager.PreferredBackBufferHeight;
        }

        private void FindHighestSupportedResolution()
        {
            displayM = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        }

        private void ToggleFullScreen()
        {
            if (!this.isFullScreen)
            {
                vwidth = width;
                vheight = height;
                this.isFullScreen = true;
                FindHighestSupportedResolution();
                SetResolution(displayM.Width, displayM.Height);
            }
            else
            {
                this.isFullScreen = false;
                SetResolution(vwidth, vheight);
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F11))
            {
                ToggleFullScreen();
            }
        }

        #endregion
    }
}