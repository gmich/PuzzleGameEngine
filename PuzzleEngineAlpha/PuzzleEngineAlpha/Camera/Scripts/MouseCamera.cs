using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Scripts
{
    using Input;

    public class MouseCamera : ICameraScript
    {

        #region Declarations

        private Vector2 initialPos;
        private Vector2 worldLocation;
        private Vector2 velocity;
        private Vector2 deSquareerating;
        private bool scrolling;
        private Camera camera;

        #endregion

        #region Constructor

        public MouseCamera(Camera camera)
        {
            this.worldLocation = camera.Position;
            deSquareerating = initialPos = velocity = Vector2.Zero;
            scrolling = false;
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
            get;
            set;
        }


        public Vector2 WorldLocation
        {
            get
            {
                return worldLocation;
            }
            set
            {
                 worldLocation.X = MathHelper.Clamp(value.X, Camera.ViewPortWidth,
                                             Camera.WorldSize.X- Camera.ViewPortWidth / 2);
                 worldLocation.Y = MathHelper.Clamp(value.Y, Camera.ViewPortHeight / 2,
                                             Camera.WorldSize.Y - Camera.ViewPortHeight / 2);
            }
        }

        #endregion

        #region Scrolling

        private void ReduceVector(ref Vector2 vector)
        {
            float reduceAmount = 15.0f;
            float maxAcceleration = 2000f;
            if (vector.X > 0)
                vector.X = MathHelper.Clamp(vector.X - reduceAmount, 0, maxAcceleration);
            else
                vector.X = MathHelper.Clamp(vector.X + reduceAmount, -maxAcceleration, 0);

            if (vector.Y > 0)
                vector.Y = MathHelper.Clamp(vector.Y - reduceAmount, 0, maxAcceleration);
            else
                vector.Y = MathHelper.Clamp(vector.Y + reduceAmount, -maxAcceleration, 0);
        }

        public void RepositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;
            int screenLocY = (int)Camera.WorldToScreen(worldLocation).Y;

            if (screenLocY > Camera.ViewPortHeight / 2)
            {
                Camera.Move(new Vector2(0, screenLocY - Camera.ViewPortHeight / 2));
            }
            if (screenLocY < Camera.ViewPortHeight / 2)
            {
                Camera.Move(new Vector2(0, screenLocY - Camera.ViewPortHeight / 2));
            }

            if (screenLocX > Camera.ViewPortWidth / 2)
            {
                Camera.Move(new Vector2(screenLocX - Camera.ViewPortWidth / 2, 0));
            }

            if (screenLocX < Camera.ViewPortWidth / 2)
            {
                Camera.Move(new Vector2(screenLocX - Camera.ViewPortWidth / 2, 0));
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (InputHandler.RightButtonIsClicked() && !scrolling)
            {
                scrolling = true;
                initialPos = InputHandler.MousePosition;
            }
            else if (InputHandler.RightButtonIsClicked() && scrolling)
            {
                velocity = initialPos - InputHandler.MousePosition;
                initialPos = InputHandler.MousePosition;
            }
            else if (!InputHandler.RightButtonIsClicked())
            {
                if (scrolling)
                    deSquareerating = velocity * 50;
                velocity = (float)gameTime.ElapsedGameTime.TotalSeconds * deSquareerating;
                ReduceVector(ref deSquareerating);
                scrolling = false;
            }

            if (velocity != Vector2.Zero)
            {
                WorldLocation += velocity;
                RepositionCamera();
            }

        }

        #endregion
    }
}