using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Input
{
    public class InputUtilities
    {
        #region Declarations

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Dictionary<string, Keys> keyMapper;

        #endregion

        #region Constructor

        public InputUtilities()
        {
            keyMapper = new Dictionary<string, Keys>();
            InitializeKeyMapper();
        }

        #endregion

        #region Private Helper Methods

        void InitializeKeyMapper()
        {
            keyMapper.Add("ArrowUp", Keys.Up);
            keyMapper.Add("ArrowDown", Keys.Down);
            keyMapper.Add("ArrowLeft", Keys.Left);
            keyMapper.Add("ArrowRight", Keys.Right);

            //TODO: finish key mapping
        }

        #endregion

        #region Static Helper Methods

        //TODO: update this to handle exceptions correctly
        public static Keys GetKey(string key)
        {
            try
            {
                return (keyMapper[key]);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + " - Key not correctly mapped in InputUtilities");
                return Keys.NumPad0;
            }
        }
        #endregion
    }
}
