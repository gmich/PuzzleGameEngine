using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzlePrototype.Level
{
    using Animations;

    abstract class AGameObect
    {
        protected AGameObect(AnimationStrip animationStrip, int width = LevelManager.TileHeight, int height = LevelManager.TileWidth);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
