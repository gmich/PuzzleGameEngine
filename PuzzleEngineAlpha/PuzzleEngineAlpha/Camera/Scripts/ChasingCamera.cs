using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Camera.Scripts
{
    using Handlers;

    public class ChasingCamera : ICameraScript
    {

        #region Declarations

        Camera camera;
        float distance;
        Vector2 targetLocation;

        #endregion

        #region Constructor

        public ChasingCamera(Vector2 targetLocation, Camera camera)
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

        public float ChaseStep
        {
            get
            {
                return 9.0f;
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

        private void RepositionCamera(float timePassed)
        {
            int screenLocX = (int)camera.WorldToScreen(camera.Position).X;
            int screenLocY = (int)camera.WorldToScreen(camera.Position).Y;
            Vector2 angle = TargetLocation - camera.WindowCenter;

            angle.Normalize();

            camera.Move(angle * distance * timePassed);
         
        }

        public void Update(GameTime gameTime)
        {

            distance = Vector2.Distance(TargetLocation, camera.WindowCenter);
            distance *= ChaseStep;

            if (distance < 0.001f)
                distance = 0.0f;

            RepositionCamera((float)gameTime.ElapsedGameTime.TotalSeconds);

        }

        #endregion

    }
}
