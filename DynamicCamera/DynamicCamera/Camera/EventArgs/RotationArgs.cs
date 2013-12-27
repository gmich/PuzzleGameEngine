using System;

namespace DynamicCamera
{
    public class RotationArgs : EventArgs
    {
        #region Constructor

        public RotationArgs(int rotationState)
        {
            this.RotationState = rotationState;
        }

        #endregion

        #region Properties

        public int RotationState { get; private set; }

        #endregion
    }
}

