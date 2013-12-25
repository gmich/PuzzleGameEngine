using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DynamicCamera
{
    using Input;

    public class CameraHandler
    {
        #region Declarations

        List<ICameraScript> cameraScripts;

        #endregion

        #region Constructor

        public CameraHandler()
        {
            cameraScripts = new List<ICameraScript>();
        }

        #endregion

        #region Properties

        float ZoomStep
        {
            get
            {
                return 0.01f;
            }
        }

        float RotationStep
        {
            get
            {
                return 0.01f;
            }
        }

        #endregion

        #region Helper Methods

        void HandleZoom(ICameraScript cameraScript)
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                cameraScript.Camera.Zoom += ZoomStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                cameraScript.Camera.Zoom -= ZoomStep;
        }

        void HandleRotation(ICameraScript cameraScript)
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                cameraScript.Camera.Rotation += RotationStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                cameraScript.Camera.Rotation -= RotationStep;
        }

        public void AddCameraScript(ICameraScript script)
        {
            this.cameraScripts.Add(script);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (ICameraScript script in cameraScripts)
            {
                HandleZoom(script);
                HandleRotation(script);

                script.Update(gameTime);
            }
        }

        #endregion

    }
}
