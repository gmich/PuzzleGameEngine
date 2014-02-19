using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.Areas
{
    using Input.Scripts;

    public class SampleArea : IGUIArea
    {
        #region Declarations

        List<AGUIComponent> components;
        IMenuInputScript inputScript;

        #endregion

        #region Constructor

        public SampleArea(IMenuInputScript inputScript)
        {
            components = new List<AGUIComponent>();
            FocusedComponentID = -1;
            this.inputScript = inputScript;
        }
        
        #endregion 
            
        #region Properties

        public AGUIComponent FocusedComponent
        {
            get { return components[FocusedComponentID]; }
        }

        int focusedComponentID;
        public int FocusedComponentID
        {
            get
            {
                return focusedComponentID;
            }
            set
            {
                focusedComponentID += value;

                if (focusedComponentID > components.Count - 1)
                    focusedComponentID = 0;
                else if (focusedComponentID < 0)
                    focusedComponentID = components.Count - 1;
            }
        }

        #endregion


        public void AddElement(AGUIComponent component)
        {
            components.Add(component);
        }

        public void Enumerate()
        {
            if(!inputScript.Next && !inputScript.Previous)
            {
                if (inputScript.Next)
            {
                components[focusedComponentID].IsFocused = false;
                focusedComponentID++;
                components[focusedComponentID].IsFocused = true;
            
            }
                if (inputScript.Previous)
                {
                    components[focusedComponentID].IsFocused = false;
                    focusedComponentID++;
                    components[focusedComponentID].IsFocused = true;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            Enumerate();
            foreach (AGUIComponent component in components)
                component.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AGUIComponent component in components)
                component.Draw(spriteBatch);
        }

        void IGUIArea.AddElement(AGUIComponent element)
        {
            throw new NotImplementedException();
        }

        void IGUIArea.Enumerate()
        {
            throw new NotImplementedException();
        }

        void IGUIArea.Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        void IGUIArea.Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
