using System;
using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Animations
{
    public class SmoothTransaction 
    {
        public SmoothTransaction(float initialValue,float step,float minValue,float maxValue)       
        {
            this.Value = initialValue;
            this.Step = step;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        #region Properties

        float value;
        public float Value
        {
            get
            {
                return MathHelper.Clamp(value, MinValue, MaxValue);
            }
            private set
            {
                this.value = value;
            }
        }

        float Step
        {
            get;
            set;
        }

        float MinValue
        {
            get;
            set;
        }

        float MaxValue
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        public void Increase(GameTime gameTime)
        {
            this.Value += Step * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Decrease(GameTime gameTime)
        {
            this.Value -= Step * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        #endregion
    }
}