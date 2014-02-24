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

        #region Intersection

        public bool Intersects(Vector2 otherLocation)
        {
            return ((Camera.ScreenRectangle.X < otherLocation.X && Camera.ScreenRectangle.Y < otherLocation.Y)
                    && (Camera.ScreenRectangle.X + Camera.ScreenRectangle.Width > otherLocation.X && Camera.ScreenRectangle.Y + Camera.ScreenRectangle.Height > otherLocation.Y));
        }

        #endregion

        #region Helper Methods

        void HandleZoom(float step)
        {  
            if ((InputHandler.IsKeyDown(ConfigurationManager.Config.ZoomIn) || InputHandler.IsWheelMovingUp()) && this.Intersects(InputHandler.MousePosition))
                Scale += step;

            else if ((InputHandler.IsKeyDown(ConfigurationManager.Config.ZoomOut) || InputHandler.IsWheelMovingDown() )&& this.Intersects(InputHandler.MousePosition))
                Scale -= step; 
        }

        #endregion

        public void Update(GameTime gameTime)
        {  
            HandleZoom(scaleStep);
            Camera.Zoom = Scale;
        }

    }
}