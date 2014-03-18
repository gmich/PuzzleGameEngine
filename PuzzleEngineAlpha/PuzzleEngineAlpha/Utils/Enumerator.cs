using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Utils
{
    public class Enumerator
    {
        #region Constructors

        public Enumerator(int Count,int Value,Keys NextKey,Keys PreviousKey)
        {
            this.Count = Count;
            this.Value = Value;
            this.NextKey = NextKey;
            this.PreviousKey = PreviousKey;
        }

        public Enumerator(int Count, int Value)
        {
            this.Count = Count;
            this.Value = Value;
        }

        #endregion

        #region Enumeration Change Event

        public delegate void EnumrationHandler();
        public event EnumrationHandler NewValue;

        void OnNewValue(EventArgs e)
        {
            if (NewValue != null)
                NewValue();
        }

        #endregion

        #region Properties

        public Keys NextKey
        {
            get;
            set;
        }

        public Keys PreviousKey
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        int value;
        public int Value
        {
            get
            {
                return (int)MathHelper.Clamp(value, 0, Count - 1);
            }
            set
            {
                if (value > Count - 1)
                    this.value = 0;
                else if (value < 0)
                    this.value = Count - 1;
                else
                    this.value = value;
            }
        }

        #endregion

        #region Public Enumeration Methods

        public void Next()
        {
            Value++;
            OnNewValue(EventArgs.Empty);
        }

        public void Previous()
        {
            Value--;
            OnNewValue(EventArgs.Empty);
        }

        #endregion

        #region Update

        public void Update()
        {
            if (Input.InputHandler.IsKeyPressed(PreviousKey))
                Previous();
            else if (Input.InputHandler.IsKeyPressed(NextKey))
                Next();
        }

        public void UpdateOnClickDown()
        {
            if (Input.InputHandler.IsKeyReleased(PreviousKey))
                Previous();
            else if (Input.InputHandler.IsKeyReleased(NextKey))
                Next();
        }

        #endregion
    }
}
