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
            private set
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
        }

        public void Previous()
        {
            Value--;
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


        #endregion
    }
}
