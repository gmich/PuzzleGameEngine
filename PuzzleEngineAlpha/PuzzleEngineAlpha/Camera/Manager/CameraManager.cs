using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Managers
{
    using Handlers;
    using Scripts;
    using Input;

    public class CameraManager
    {
        #region Declarations

        List<ICameraHandler> cameraHandlers;
        ICameraScript cameraScript;
        List<InputConfiguration> inputConfiguration;

        #endregion

        public CameraManager()
        {
            inputConfiguration = new List<InputConfiguration>();
            cameraHandlers = new List<ICameraHandler>(); 
        }

        #region Properties

        public Vector2 Position
        {
            get
            {
                return cameraScript.Camera.Position;
            }
        }

        public Vector2 TargetLocation
        {
            set
            {
                cameraScript.TargetLocation = value;
            }
        }

        public Camera Camera
        {
            get
            {
                return cameraScript.Camera;
            }
        }

        #endregion

        #region Helper Methods

        public void AddCameraHandler(ICameraHandler cameraHandler)
        {
            cameraHandlers.Add(cameraHandler);
            cameraHandler.Camera = cameraScript.Camera;
        }

        public void SetCameraScript(ICameraScript cameraScript)
        {
            this.cameraScript = cameraScript;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            cameraScript.Update(gameTime);

            foreach (ICameraHandler camerahandler in cameraHandlers)
                camerahandler.Update(gameTime);
        }
    }
}
