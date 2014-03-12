using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Camera.Scripts
{
    using Handlers;

    public class FreeRoam : ICameraScript
    {

       #region Declarations

        Camera camera;
        Vector2 targetLocation;
        Input.Scripts.MovementScript movementScript;
        Vector2 velocity;
        float distance;

        #endregion

        #region Constructor

        public FreeRoam(Vector2 targetLocation, Camera camera)
        {
            this.targetLocation = targetLocation;
            this.camera = camera;
            movementScript = new Input.Scripts.MovementScript();
            velocity = Vector2.Zero;
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

        Vector2 dummyLocation;
        public Vector2 TargetLocation
        {
            get
            {
                return dummyLocation;
            }
            set
            {
                dummyLocation = value;
            }
        }
        
        #endregion

        #region Helper Methods

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
            Vector2 angle = targetLocation - camera.WindowCenter;

            if (angle != Vector2.Zero)
                angle.Normalize();

            camera.Move(angle * distance * timePassed);
        }

        public void Update(GameTime gameTime)
        {
            float step = 700.0f;
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (movementScript.MoveUp)
            {
                targetLocation += new Vector2(0, -step) * elapsedTime;
            }
            else if (movementScript.MoveDown)
            {
                targetLocation += new Vector2(0, +step) * elapsedTime;
            }
            if (movementScript.MoveLeft)
            {
                targetLocation += new Vector2(-step, 0) * elapsedTime;
            }
            else if (movementScript.MoveRight)
            {
                targetLocation += new Vector2(step, 0) * elapsedTime;
            }


            AdjustLocationInBoundaries(ref targetLocation);
            distance = Vector2.Distance(targetLocation, camera.WindowCenter);
            distance *= 5.0f;

            if (distance < 0.001f)
                distance = 0.0f;


            RepositionCamera((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        #endregion

    }
}
