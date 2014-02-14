using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DynamicCamera.Camera.Manager
{
    using Handlers;
    using Scripts;

    public class CameraManager
    {
        #region Declarations

        List<ICameraHandler> cameraHandlers;
        ICameraScript cameraScript;

        #endregion

        public CameraManager()
        {
            cameraHandlers = new List<ICameraHandler>(); 
        }

        #region Helper Methods

        public void AddCameraMan(ICameraHandler cameraHandler)
        {
            cameraHandlers.Add(cameraHandler);
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
                camerahandler.Update(gameTime, cameraScript.Camera);
        }
    }
}
