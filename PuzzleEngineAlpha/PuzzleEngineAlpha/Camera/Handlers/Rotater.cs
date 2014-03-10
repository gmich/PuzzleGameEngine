using System;
using System.Collections.Generic;
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
        Queue<bool> rotationStates;

        #endregion

        #region Constructor

        public Rotater(float rotationAmount, float rotationCycle, int rotationSteps)
        {
            rotation = new Rotation(rotationAmount, rotationCycle, rotationSteps);
            rotationState = 0;
            rotationStates = new Queue<bool>();
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
            bool? state = HandleRotation();
            if (state != null)
                rotationStates.Enqueue((bool)state);

            if (rotationStates.Count > 0 && !this.rotation.IsActive)
                rotation.RotateEntity(rotationStates.Dequeue());

            rotation.UpdateRotation(rotationState);

            Camera.RotationState = rotationState;
            Camera.Rotation = rotation.Value;
        }

    }
}