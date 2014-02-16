using System;
using Microsoft.Xna.Framework.Input;

namespace DynamicCamera.Input
{
    public class InputConfiguration
    {

        public static int GameCameraRotationState
        {
            get;
            set;
        }

        public Keys Up
        {
            get;
            set;
        }

        public Keys Down
        {
            get;
            set;
        }

        public Keys Left
        {
            get;
            set;
        }

        public Keys Right
        {
            get;
            set;
        }

        public void InitializeKeys(int state)
        {
            switch (state)
            {
                case 1:
                    Up = Keys.Right;
                    Down = Keys.Left;
                    Left = Keys.Up;
                    Right = Keys.Down;
                    break;
                case 2:
                    up = Keys.Down;
                    down = Keys.Up;
                    left = Keys.Right;
                    right = Keys.Left;
                    break;
                case 3:
                    up = Keys.Left;
                    down = Keys.Right;
                    left = Keys.Down;
                    right = Keys.Up;
                    break;
                default:
                    up = Keys.Up;
                    down = Keys.Down;
                    left = Keys.Left;
                    right = Keys.Right;
                    break;

            }
        }
    }
}
