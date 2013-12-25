using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DynamicCamera
{
    public class ChasingCamera : ICameraScript
    {

        #region Declarations

        Camera camera;
        float distance;
        Vector2 targetLocation;

        #endregion

        #region Constructor

        public ChasingCamera(Vector2 targetLocation,Vector2 viewportSize)
        {
            this.TargetLocation = targetLocation;
            camera = new Camera(targetLocation, viewportSize);
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

        private void RepositionCamera(float timePassed)
        {
            int screenLocX = (int)camera.WorldToScreen(camera.Position).X;
            int screenLocY = (int)camera.WorldToScreen(camera.Position).Y;
            Vector2 angle = targetLocation - camera.WorldCenter;

            angle.Normalize();

            camera.Move(angle * distance * timePassed);
            /*
            if (screenLocY > camera.ViewPortHeight / 2)
            {
                camera.Move(angle * distance * timePassed);
            }
            if (screenLocY < camera.ViewPortHeight / 2)
            {
                camera.Move(angle * distance * timePassed);
            }

            if (screenLocX > camera.ViewPortWidth / 2)
            {
                camera.Move(angle * distance * timePassed);
            }

            if (screenLocX < camera.ViewPortWidth / 2)
            {
                camera.Move(angle * distance * timePassed);
            }
             */
        }


        public void Update(GameTime gameTime)
        {
            
            distance = Vector2.Distance(targetLocation, camera.WorldCenter);
            distance *= 5;

            if (distance < 0.001f)
                distance = 0.0f;

            RepositionCamera(gameTime.ElapsedGameTime.Seconds);
        }

        #endregion

    }
}
