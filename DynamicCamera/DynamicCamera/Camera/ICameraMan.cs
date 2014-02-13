using Microsoft.Xna.Framework;

namespace DynamicCamera
{
    public interface ICameraMan
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
