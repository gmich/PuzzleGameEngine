using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Handlers
{

    public interface ICameraHandler
    {

        float Value
        {
            get;
        }
        
        void Update(GameTime gameTime);
    }
}
