using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.Areas
{
    interface IGUIArea
    {
        AGUIComponent FocusedComponent
        {
            get;
        }

        void AddElement(AGUIComponent component);

        void Enumerate();
  
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
