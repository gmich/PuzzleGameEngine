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
        List<ICameraMan> cameraMen;

        #endregion

        #region Constructor

        public ChasingCamera(Vector2 targetLocation,Vector2 viewportSize,Vector2 cameraSize)
        {
            this.TargetLocation = targetLocation;
            camera = new Camera(targetLocation, viewportSize, cameraSize);
            cameraMen = new List<ICameraMan>();
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
                return 8.0f;
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

        public void AddCameraMan(ICameraMan cameraMan)
        {
            cameraMen.Add(cameraMan);
        }

        private void RepositionCamera(float timePassed)
        {
            int screenLocX = (int)camera.WorldToScreen(camera.Position).X;
            int screenLocY = (int)camera.WorldToScreen(camera.Position).Y;
            Vector2 angle = targetLocation - camera.WindowCenter;

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

            distance = Vector2.Distance(targetLocation, camera.WindowCenter);
            distance *= ChaseStep;

            RepositionCamera((float)gameTime.ElapsedGameTime.TotalSeconds);

            foreach (ICameraMan cameraman in cameraMen)
                cameraman.Update(gameTime,this.Camera);
        }

        #endregion

    }
}
