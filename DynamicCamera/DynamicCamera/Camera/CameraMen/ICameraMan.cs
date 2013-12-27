using System;
using Microsoft.Xna.Framework;

namespace DynamicCamera
{
    public interface ICameraMan
    {

        event EventHandler Triggered;

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
