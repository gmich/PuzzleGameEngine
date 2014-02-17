using Microsoft.Xna.Framework;

namespace PuzzleEngineAlpha.Camera.Handlers
{

    public interface ICameraHandler
    {

        Camera Camera
        {
            set;
        }
        
        void Update(GameTime gameTime);
    }
}
