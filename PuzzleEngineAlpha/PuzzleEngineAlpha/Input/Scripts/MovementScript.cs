using System;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Input.Scripts
{
    public class MovementScript
    {

        public Keys GetMovementKey(int state)
        {

            switch (ValidState(state))
            {          
                case 0:
                    return ConfigurationManager.Config.Up;
                case 1:
                    return ConfigurationManager.Config.Right;
                case 2:
                    return ConfigurationManager.Config.Down;
                case 3: 
                    return ConfigurationManager.Config.Left;      

            }
            return Keys.F12;

        }

        public int RotationState
        {
            get;
            set;
        }

        public int ValidState(int state)
        {
            if (state > MaxRotationState)
                return (state - MaxRotationState-1);
            else
                return state;
        }
        
        //TODO:  get info from camera
        int MaxRotationState
        {
            get
            {
                return 3;
            }
        }             

        public bool MoveUp
        {
            get
            {
                Keys key = GetMovementKey(RotationState);
                if (key != Keys.F12)
                    return (InputHandler.IsKeyDown(key));
                return false;
            }
        }

        public bool MoveRight
        {
            get
            {
                Keys key = GetMovementKey(RotationState+1);
                if (key != Keys.F12)
                    return (InputHandler.IsKeyDown(key));
                return false;
            }
        }

        public bool MoveDown
        {
            get
            {
                Keys key = GetMovementKey(RotationState + 2);
                if (key != Keys.F12)
                    return (InputHandler.IsKeyDown(key));
                return false;
            }
        }

        public bool MoveLeft
        {
            get
            {
                Keys key = GetMovementKey(RotationState + 3);
                if (key != Keys.F12)
                    return (InputHandler.IsKeyDown(key));
                return false;
            }
        }
    }
}