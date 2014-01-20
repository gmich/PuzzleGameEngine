using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Components
{
    using Actions;
    //decorator
    public abstract class AButton : IGUIArea
    {
        IGUIArea area;
        List<IAction> actions;

        public AButton(IGUIArea areaToRender)
        {
            this.area = areaToRender;
        }

        public AButton()
        {      
        }

        void StoreAndExecute(IAction action)
        {
            this.actions.Add(action);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public Microsoft.Xna.Framework.Vector2 Position
        {
            get { throw new NotImplementedException(); }
        }
    }
}
