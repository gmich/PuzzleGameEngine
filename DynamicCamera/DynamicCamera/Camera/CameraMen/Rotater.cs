using System;
using Microsoft.Xna.Framework;

namespace DynamicCamera
{
    using Input;

    public class Rotater : ICameraMan
    {
        #region Declarations

        bool? clockwise;
        bool active;
        float rotationRate;
        int rotationTicksRemaining;
        float rotationAmount;
        int rotationSteps;
        int rotationState;

        #endregion

        #region Events

        public event EventHandler Triggered;

        protected virtual void OnTriggered(EventArgs e)
        {
            EventHandler handler = Triggered;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Constructor

        public Rotater(float rotationAmount, float rotationCycle,int rotationSteps)
        {
            rotationRate = rotationCycle/rotationSteps;
            this.rotationAmount = rotationAmount;
            rotationTicksRemaining = rotationSteps;
            this.rotationSteps = rotationSteps;
            active = false;
            clockwise = null;
            rotationState = 0;
        }

        #endregion

        #region Properties

        public bool IsActive
        {
            get { return active; }
        }

        public float Value
        {
            get
            {
                if (IsActive)
                {
                    UpdateRotation();
                    if (clockwise==true)
                        rotationAmount += rotationRate;
                    else if (clockwise == false)
                        rotationAmount -= rotationRate;

                    return rotationAmount;
                }
                else
                    return rotationAmount;
            }
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

                return false;
            }
                return null;
        }

        public void RotateEntity(bool? clockwise)
        {
            if (clockwise != null)
            {
                this.clockwise = clockwise;
                rotationTicksRemaining = rotationSteps;
                active = true;
            }
        }

        public void UpdateRotation()
        {
            rotationTicksRemaining = (int)MathHelper.Max(0, rotationTicksRemaining - 1);

            if (rotationTicksRemaining == 0)
            {
                OnTriggered(new RotationArgs(rotationState));
                active = false;
            }
        }

        #endregion

        public void Update(GameTime gameTime,Camera camera)
        {
            if(!this.IsActive)
                RotateEntity(HandleRotation());

            camera.Rotation = Value;
        }
    }
}
