using System;
using Microsoft.Xna.Framework;

namespace DynamicCamera.Camera.Handlers
{
    using Input;
    using Animations;
    using Camera;

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
              
        #region Public Methods

        public bool? HandleRotation()
        {
            if ((InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Q)
                || InputHandler.ForwardButtonIsPressed()))
            {
                if (rotationState == 3)
                    rotationState = 0;
                else rotationState++;

                return true;
            }
            else if ((InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.W)
                || InputHandler.BackButtonIsPressed()))
            {
                if (rotationState == 0)
                    rotationState = 3;
                else rotationState--;

                if ((InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Q)
                    || InputHandler.ForwardButtonIsPressed()))
                    return true;
                else if ((InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.W)
                    || InputHandler.BackButtonIsPressed()))
                    return false;

                return null;
            }
            return null;
        }

        #endregion

        public void Update(GameTime gameTime, Camera camera)
        {
            rotation.RotateEntity(HandleRotation());
            rotation.UpdateRotation();
            camera.Rotation = rotation.Value;
        }
    }
}