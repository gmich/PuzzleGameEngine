using Microsoft.Xna.Framework;

namespace DynamicCamera.Camera.Handlers
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
