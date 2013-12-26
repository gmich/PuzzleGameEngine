using System;
using Microsoft.Xna.Framework;

namespace DynamicCamera
{
    public interface ICameraScript
    {
        Camera Camera
        {
            get;
        }

        Vector2 TargetLocation
        {
            get;
            set;
        }

        void Update(GameTime gameTime);

    }
}
