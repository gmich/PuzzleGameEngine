using System;
using Microsoft.Xna.Framework.Graphics;
 
namespace PuzzleEngineAlpha.Actions
{
    using Resolution;

    public class ApplyResolutionAction : IAction
    {
        ResolutionHandler resolutionHandler;
        DisplayMode displayMode;

        public ApplyResolutionAction(ResolutionHandler resolutionHandler,DisplayMode displayMode)
        {
            this.resolutionHandler = resolutionHandler;
            this.displayMode = displayMode;
        }

        public void Execute()
        {
            resolutionHandler.SetResolution(displayMode.Width, displayMode.Height);
        }
    }
}
