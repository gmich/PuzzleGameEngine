using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzlePrototype.Level
{
    public interface Tile
    {
        bool Passable
        {
            get;
            set;
        }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
        
    }
}
