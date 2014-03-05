using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Animations
{
    using Input;
    using EventArguments;

    public class Rotation
    {
        #region Declarations

        bool? clockwise;
        bool active;
        float rotationRate;
        int rotationTicksRemaining;
        float rotationAmount;
        int rotationSteps;

        #endregion

        #region Constructor

        public Rotation(float rotationAmount, float rotationCycle, int rotationSteps)
        {
            rotationRate = rotationCycle/(rotationSteps-1);
            this.rotationAmount = rotationAmount;
            rotationTicksRemaining = rotationSteps;
            this.rotationSteps = rotationSteps;
            active = false;
            clockwise = null;
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

        #region Public Methods
        
        public void RotateEntity(bool? clockwise)
        {
            if (clockwise != null && !this.IsActive)
            {
                this.clockwise = clockwise;
                rotationTicksRemaining = rotationSteps;
                active = true;
            }
        }

        public void UpdateRotation(int rotationState)
        {
            rotationTicksRemaining = (int)MathHelper.Max(0, rotationTicksRemaining - 1);

            if (rotationTicksRemaining == 0)
            {
                OnTriggered(new RotationStateArgs(rotationState));
                active = false;
            }
        }

        #endregion
        
    }
}