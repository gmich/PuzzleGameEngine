using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Components.Areas
{
    using Utils;

    public class ComponentEnumerator
    {
        #region Declarations

        List<AGUIComponent> components;
        Enumerator enumerator;
        Keys triggerKey;

        #endregion

        #region Constructor

        public ComponentEnumerator(Keys nextKey, Keys previousKey, Keys triggerKey)
        {
            components = new List<AGUIComponent>();
            enumerator = new Enumerator(components.Count, 0);
            enumerator.NextKey = nextKey;
            enumerator.PreviousKey = previousKey;
            this.triggerKey = triggerKey;
            enumerator.NewValue += EnumerationHandler;
        }

        #endregion

        #region Add / Remove GUI Components

        public void AddGUIComponent(AGUIComponent component)
        {
            components.Add(component);
            enumerator.Count = components.Count;
        }

        #endregion

        #region Manipulate Focus
        
        public void SetCurrentValue(int value)
        {
            this.enumerator.Value=value;
        }

        #endregion

        #region Enumeration Handling

        public void EnumerationHandler()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].IsFocused = false;
                components[i].AutoUpdate = false;
            }
            components[enumerator.Value].IsFocused = true;
        }

        #endregion

        #region Selection Handling

        public void HandleSelection()
        {
            enumerator.UpdateOnClickDown();
            components[enumerator.Value].IsFocused = true;
            if (Input.InputHandler.IsKeyPressed(triggerKey))
            {
                for (int i = 0; i < components.Count; i++)
                {
                    if (components[i].IsFocused)
                        components[i].OnRelease();
                }
            }
        }

        #endregion
    }
}
