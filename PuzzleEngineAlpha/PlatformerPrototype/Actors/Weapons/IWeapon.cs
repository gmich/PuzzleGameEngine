using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerPrototype.Actors.Weapons
{
    public interface IWeapon
    {
        void Shoot(Vector2 location, Vector2 velocity);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
