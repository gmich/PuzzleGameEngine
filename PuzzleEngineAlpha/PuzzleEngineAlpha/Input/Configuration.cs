using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Input
{
    public class Configuration
    {

        #region Properties

        public int GameCameraRotationState
        {
            get;
            set;
        }

        #region Game Keys

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

        public Keys EnumerateVNext
        {
            get
            {
                return Keys.Down;
            }          
        }

        public Keys EnumerateVPrevious
        {
            get
            {
                return Keys.Up;
            }
        }

        public Keys ToggleDiagnostics
        {
            get
            {
                return Keys.F1;
            }
        }

        public Keys Trigger
        {
            get
            {
                return Keys.Enter;
            }
        }

        #endregion

        #endregion
    
        }
    }


