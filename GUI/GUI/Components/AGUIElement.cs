using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Components
{
    using Actions;
    using Input;

    public abstract class AGUIElement
    {
        #region Constructor

        protected AGUIElement()
        {
            mouseOverActions = new List<IAction>();
            mouseClickActions = new List<IAction>();
            mouseReleaseActions = new List<IAction>();
            mouseLeaveActions = new List<IAction>();
            mouseIsOverButton = false;
            mouseIsClickingButton = false;
            mouseCanRelease =false;
        }

        #endregion

        #region Update and Draw

        public virtual void Update(GameTime gameTime)
        {
            mouseIsOver();
            mouseIsClicking();
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

        #region Custom Mouse Events

        void OnMouseOver()
        {
            foreach (IAction action in mouseOverActions)
                action.Execute();
        }

        void OnMouseLeave()
        {
            foreach (IAction action in mouseLeaveActions)
                action.Execute();
        }
        void OnMouseClick()
        {
            foreach (IAction action in mouseClickActions)
                action.Execute();
        }

        void OnMouseRelease()
        {
            foreach (IAction action in mouseReleaseActions)
                action.Execute();
        }

        #endregion

        #region Custom Event Actions

        #region Declarations

        List<IAction> mouseOverActions;
        List<IAction> mouseClickActions;
        List<IAction> mouseReleaseActions;
        List<IAction> mouseLeaveActions;

        #endregion

        #region Store Action

        public void StoreAndExecuteOnMouseOver(IAction action)
        {
            mouseOverActions.Add(action);
        }

        public void StoreAndExecuteOnMouseClick(IAction action)
        {
            mouseClickActions.Add(action);
        }

        public void StoreAndExecuteOnMouseRelease(IAction action)
        {
            mouseReleaseActions.Add(action);
        }

        public void StoreAndExecuteOnMouseLeave(IAction action)
        {
            mouseLeaveActions.Add(action);
        }

        #endregion

        #region Remove Action

        public void RemoveActionOnMouseOver(IAction action)
        {
            for (int i = 0; i < mouseOverActions.Count; i++)
            {
                if (mouseOverActions[i] == action)
                {
                    mouseOverActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseClick(IAction action)
        {
            for (int i = 0; i < mouseClickActions.Count; i++)
            {
                if (mouseClickActions[i] == action)
                {
                    mouseClickActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseRelease(IAction action)
        {
            for (int i = 0; i < mouseReleaseActions.Count; i++)
            {
                if (mouseReleaseActions[i] == action)
                {
                    mouseReleaseActions.RemoveAt(i);
                    break;
                }
            }
        }

        public void RemoveActionOnMouseLeave(IAction action)
        {
            for (int i = 0; i < mouseLeaveActions.Count; i++)
            {
                if (mouseLeaveActions[i] == action)
                {
                    mouseLeaveActions.RemoveAt(i);
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

        protected bool mouseIsOverButton;
        void mouseIsOver()
        {
            if (this.Intersects(InputHandler.MousePosition) && !mouseIsOverButton)
            {
                OnMouseOver();
                mouseIsOverButton = true;
            }
            else if (!this.Intersects(InputHandler.MousePosition) && mouseIsOverButton)
            {
                OnMouseLeave();
                mouseIsOverButton = false;
            }
        }

        protected bool mouseIsClickingButton;
        protected bool mouseCanRelease;
        void mouseIsClicking()
        {
            if (mouseIsOverButton && InputHandler.LeftButtonIsClicked() && !mouseIsClickingButton)
            {
                OnMouseClick();
                mouseIsClickingButton = true;
                mouseCanRelease = true;
            }
            else if (InputHandler.LeftButtonIsClicked() && !mouseIsOverButton)
            {
                mouseIsClickingButton = true;
            }
            else if (!InputHandler.LeftButtonIsClicked())
            {
                mouseIsClickingButton = false;
                if (mouseCanRelease && mouseIsOverButton)
                    OnMouseRelease();
                mouseCanRelease = false;
            }
        }

        #endregion
    }
}
