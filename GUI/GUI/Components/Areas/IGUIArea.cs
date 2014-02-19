using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Components.Areas
{
    interface IGUIArea
    {
        AGUIElement SelectedElement
        {
            get;
        }

        void AddElement(AGUIComponent element);

        void Enumerate();
  
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
