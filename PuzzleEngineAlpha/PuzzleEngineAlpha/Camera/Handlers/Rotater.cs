using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Handlers
{
    using Input;
    using Animations;

    public class Rotater : ICameraHandler
    {
        #region Declarations

        Rotation rotation;
        int rotationState;

        #endregion

        #region Constructor

        public Rotater(float rotationAmount, float rotationCycle, int rotationSteps)
        {
            rotation = new Rotation(rotationAmount, rotationCycle, rotationSteps);
            rotationState = 0;
        }

        #endregion

        #region Rotation Properties

        public Camera Camera
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public bool? HandleRotation()
        {
            if (rotation.IsActive) return null;

            if (InputHandler.IsKeyReleased(ConfigurationManager.Config.RotateClockwise))
            {
                if (rotationState == 3)
                    rotationState = 0;
                else rotationState++;

                return true;
            }
            else if (InputHandler.IsKeyReleased(ConfigurationManager.Config.RotateCounterClockwise))
            {
                if (rotationState == 0)
                    rotationState = 3;
                else rotationState--;


                return false;
            }
            return null;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            rotation.RotateEntity(HandleRotation());
            rotation.UpdateRotation(rotationState);

            Camera.RotationState = rotationState;
            Camera.Rotation = rotation.Value;
        }

    }
}