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

        public ChasingCamera(Vector2 targetLocation, Camera camera,float chaseStep)
        {
            this.TargetLocation = targetLocation;
            this.camera = camera;
            this.ChaseStep = chaseStep;
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
            get;
            set;
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

        void NormalizeLocation()
        {            int x = (int)camera.Position.X;
            int y = (int)camera.Position.Y;
            camera.Position = new Vector2(x, y);
        }

        void AdjustCameraInBoundaries()
        {
            Vector2 adjustedPosition = camera.Position;
            if (camera.WorldSize.X < Resolution.ResolutionHandler.WindowWidth)
            {
                float xOffSet = (Resolution.ResolutionHandler.WindowWidth - camera.WorldSize.X) / 2;
                adjustedPosition.X = -xOffSet;
            }
            else
            {
                adjustedPosition.X = MathHelper.Max(camera.Position.X, 0);
            }

            if (camera.WorldSize.Y < Resolution.ResolutionHandler.WindowHeight)
            {
                float yOffSet = (Resolution.ResolutionHandler.WindowHeight - camera.WorldSize.Y) / 2;
                adjustedPosition.Y = -yOffSet;
            }
            else
            {
                adjustedPosition.Y = MathHelper.Max(camera.Position.Y, 0);
            }

            camera.Position = adjustedPosition;
        }

        void AdjustLocationInBoundaries(ref Vector2 location)
        {
            if (camera.WorldSize.X < Resolution.ResolutionHandler.WindowWidth)
            {
                location.X = camera.WorldSize.X / 2;
            }
            else
            {
                location.X = MathHelper.Clamp(location.X, camera.ViewPortWidth / 2, camera.WorldSize.X - camera.ViewPortWidth / 2);
            }

            if (camera.WorldSize.Y < Resolution.ResolutionHandler.WindowHeight)
            {
                location.Y = camera.WorldSize.Y / 2;
            }
            else
            {
                location.Y = MathHelper.Clamp(location.Y, camera.ViewPortHeight / 2, camera.WorldSize.Y - camera.ViewPortHeight / 2);
            }
        }

        void RepositionCamera(float timePassed)
        {
            int screenLocX = (int)camera.WorldToScreen(camera.Position).X;
            int screenLocY = (int)camera.WorldToScreen(camera.Position).Y;
            Vector2 angle = TargetLocation - camera.WindowCenter;

            if (angle != Vector2.Zero)
                angle.Normalize();

            camera.Move(angle * distance * timePassed);

        }

        public void Update(GameTime gameTime)
        {

            AdjustLocationInBoundaries(ref targetLocation);
            distance = Vector2.Distance(TargetLocation, camera.WindowCenter);
            distance *= ChaseStep;

            if (distance < 0.001f)
                distance = 0.0f;

            RepositionCamera((float)gameTime.ElapsedGameTime.TotalSeconds);
          //  AdjustCameraInBoundaries();
        }

        #endregion

    }
}
