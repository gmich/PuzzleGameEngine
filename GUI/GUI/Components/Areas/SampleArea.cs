using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace GUI.Components.Areas
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
            FocusedElementID = -1;
            this.inputScript = inputScript;
        }
        
        #endregion 
            
        #region Properties

        public AGUIComponent FocusedElement
        {
            get { return components[FocusedElementID]; }
        }

        int focusedElementID;
        public int FocusedElementID
        {
            get
            {
                return focusedElementID;
            }
            set
            {
                focusedElementID+=value;

                if (focusedElementID > components.Count - 1)
                    focusedElementID = 0;
                else if (focusedElementID < 0)
                    focusedElementID = components.Count - 1;

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
                components[focusedElementID].IsFocused = false;
                focusedElementID++;
                components[focusedElementID].IsFocused = true;
            
            }
            else if(inputScript.Next
                }
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
