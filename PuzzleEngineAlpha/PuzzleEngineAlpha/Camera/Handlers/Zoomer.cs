using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Handlers
{
    using Input;
    using Animations;

    public class Zoomer : ICameraHandler
    {
        #region Declarations

        float scaleStep;
        float scale;
        float maxScale;
        float minScale;

        #endregion

        #region Constructor

        public Zoomer(float initialScale,float maxScale, float minScale, float scaleStep)
        {
            this.scaleStep = scaleStep;
            this.maxScale = maxScale;
            this.minScale = minScale;
            this.Scale = initialScale;
        }

        #endregion

        #region Properties

        public Camera Camera
        {
            get;
            set;
        }

        float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = MathHelper.Clamp(value, minScale, maxScale);
            }
        }

        #endregion

        #region Helper Methods

        void HandleZoom(float step)
        {
            if (InputHandler.IsKeyDown(ConfigurationManager.Config.ZoomIn))
                Scale += step;

            else if (InputHandler.IsKeyDown(ConfigurationManager.Config.ZoomOut))
                Scale -= step; 
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            HandleZoom(scaleStep * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Camera.Zoom = Scale;
        }

    }
}