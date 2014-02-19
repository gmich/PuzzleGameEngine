using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Components
{
    using Actions;
    using Input;

    /// <summary>
    /// AGUIElement is the base class for elements that respond to keyboard input
    /// </summary>
    public abstract class AGUIComponent
    {
        #region Constructor

        protected AGUIComponent()
        {
            focusActions = new List<IAction>();
            clickActions = new List<IAction>();
            releaseActions = new List<IAction>();
            focusLeaveActions = new List<IAction>();
            IsFocused = false;
            isClicking = false;
            canRelease =false;
            TriggerKey = Keys.Enter;
        }

        #endregion

        #region Update and Draw

        public virtual void Update(GameTime gameTime)
        {
            IsClicking();
        }
        
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion

        #region Rendering Properties

        protected virtual Vector2 Position
        {
            get;
            set;
        }

        protected Vector2 Size
        {
            get;
            set;
        }

        #endregion

        #region Custom Events

        protected void OnFocus()
        {
            foreach (IAction action in focusActions)
                action.Execute();
        }

        protected void OnFocusLeave()
        {
            foreach (IAction action in focusLeaveActions)
                action.Execute();
        }

        protected void OnClick()
        {
            foreach (IAction action in clickActions)
                action.Execute();
        }

        protected void OnRelease()
        {
            foreach (IAction action in releaseActions)
                action.Execute();
        }

        #endregion

        #region Custom Event Actions

        #region Declarations

        List<IAction> focusActions;
        List<IAction> clickActions;
        List<IAction> releaseActions;
        List<IAction> focusLeaveActions;

        #endregion

        #region Store Action

        public void StoreAndExecuteOnMouseOver(IAction action)
        {
            focusActions.Add(action);
        }

        public void StoreAndExecuteOnMouseClick(IAction action)
        {
            clickActions.Add(action);
        }

        public void StoreAndExecuteOnMouseRelease(IAction action)
        {
            releaseActions.Add(action);
        }

        public void StoreAndExecuteOnMouseLeave(IAction action)
        {
            focusLeaveActions.Add(action);
        }

        #endregion

        #region Remove Action

        public void RemoveActionOnMouseOver(IAction action)
        {
            for (int i = 0; i < focusActions.Count; i++)
            {
                if (focusActions[i] == action)
                {
                    focusActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseClick(IAction action)
        {
            for (int i = 0; i < clickActions.Count; i++)
            {
                if (clickActions[i] == action)
                {
                    clickActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseRelease(IAction action)
        {
            for (int i = 0; i < releaseActions.Count; i++)
            {
                if (releaseActions[i] == action)
                {
                    releaseActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseLeave(IAction action)
        {
            for (int i = 0; i < focusLeaveActions.Count; i++)
            {
                if (focusLeaveActions[i] == action)
                {
                    focusLeaveActions.RemoveAt(i);
                    break;
                }
            }
        }

        #endregion

        #endregion

        #region Intersection

        public bool Intersects(Vector2 otherLocation)
        {
            return ((Position.X <= otherLocation.X && Position.Y <= otherLocation.Y)
                    && (Position.X + Size.X >= otherLocation.X && Position.Y + Size.Y >= otherLocation.Y));
        }

        #endregion

        #region Mouse Interraction

        public Keys TriggerKey
        {
            get;
            set;
        }

        public bool IsFocused
        {
            get;
            set;
        }
        
        protected bool isClicking;
        protected bool canRelease;

        public virtual void IsClicking()
        {
            if (IsFocused && InputHandler.IsKeyDown(TriggerKey) && isClicking)
            {
                OnClick();
                isClicking = true;
                canRelease = true;
            }
            else if (InputHandler.IsKeyDown(TriggerKey) && !IsFocused)
            {
                isClicking = true;
            }
            else if (!InputHandler.IsKeyDown(TriggerKey))
            {
                isClicking = false;
                if (canRelease && IsFocused)
                    OnRelease();
                canRelease = false;
            }
        }

        #endregion
    }
}
