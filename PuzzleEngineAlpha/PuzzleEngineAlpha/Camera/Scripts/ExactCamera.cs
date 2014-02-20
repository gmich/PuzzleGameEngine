using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Camera.Scripts
{
    using Handlers;

    public class ExactCamera : ICameraScript
    {

        #region Declarations

        Camera camera;
        float distance;
        Vector2 targetLocation;

        #endregion

        #region Constructor

        public ExactCamera(Vector2 targetLocation, Camera camera)
        {
            this.TargetLocation = targetLocation;
            this.camera = camera;
        }

        #endregion

        #region Properties

        public Camera Camera
        {
            get
            {
                return camera;
            }
        }

        public Vector2 TargetLocation
        {
            get
            {
                return targetLocation;
            }
            set
            {
                targetLocation = value;
            }
        }

        #endregion

        #region Helper Methods

        private void RepositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(targetLocation).X;
            int screenLocY = (int)Camera.WorldToScreen(targetLocation).Y;

            if (screenLocY > camera.ViewPortHeight / 2)
            {
                Camera.Move(new Vector2(0, screenLocY - camera.ViewPortHeight/2));
            }
            if (screenLocY < camera.ViewPortHeight / 2)
            {
                Camera.Move(new Vector2(0, screenLocY - camera.ViewPortHeight / 2));
                
            }

            if (screenLocX > camera.ViewPortWidth / 2)
            {
                Camera.Move(new Vector2(screenLocX -  camera.ViewPortWidth / 2, 0));
 
            }

            if (screenLocX < camera.ViewPortWidth / 2)
            {
                Camera.Move(new Vector2(screenLocX -  camera.ViewPortWidth / 2, 0));

            }
        }

        public void Update(GameTime gameTime)
        {
            RepositionCamera();
        }

        #endregion

    }
}
