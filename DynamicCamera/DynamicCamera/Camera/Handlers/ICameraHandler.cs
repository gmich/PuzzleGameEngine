using Microsoft.Xna.Framework;

namespace DynamicCamera.Camera.Handlers
{
    using Camera;

    public interface ICameraHandler
    {
        bool IsActive
        {
            get;
        }

        float Value
        {
            get;
        }
        
        void Update(GameTime gameTime,Camera camera);
    }
}
