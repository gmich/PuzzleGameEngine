using System;

namespace PuzzleEngineAlpha.Animations.EventArguments
{
    public class RotationStateArgs : EventArgs
    {
        #region Constructor

        public RotationStateArgs(int rotationState)
        {
            this.RotationState = rotationState;
        }

        #endregion

        #region Properties

        public int RotationState { get; private set; }

        #endregion
    }
}
