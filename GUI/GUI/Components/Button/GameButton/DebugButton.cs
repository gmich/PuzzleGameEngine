using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Button.GameButton
{
    using Button;
    using ButtonEventArgs;

    class DebugButton : AButton
    {
        #region Constuctor

        public DebugButton(Texture2D buttonTexture, Texture2D frameTexture, Texture2D clickedButtonTexture, SpriteFont font, Vector2 location, Vector2 size, Color textColor, string text)
            : base(buttonTexture, frameTexture, clickedButtonTexture, font, location, size, textColor, text)
        {
            ShowStringEvent += ShowString;
        }

        #endregion

        #region Declare Events

        event EventHandler<ShowStringEventArgs> ShowStringEvent;

        #endregion

        #region Handle Events

        void ShowString(object sender, ShowStringEventArgs e)
        {
            Console.WriteLine(e.text);
            //this.Text = e.text;
        }

        #endregion

        #region Event Calls

        protected override void OnMouseClick(EventArgs e)
        {
            if (ShowStringEvent != null)
                ShowStringEvent(this, (ShowStringEventArgs)e);
        }

        protected override void OnMouseRelease(EventArgs e)
        {
            if (ShowStringEvent != null)
                ShowStringEvent(this, (ShowStringEventArgs)e);
        }

        protected override void OnMouseOver(EventArgs e)
        {
            if (ShowStringEvent != null)
                ShowStringEvent(this, (ShowStringEventArgs)e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ShowStringEvent != null)
                ShowStringEvent(this, (ShowStringEventArgs)e);
        }

        #endregion

        #region Override Update

        public override void Update(GameTime gameTime)
        {
            mouseIsOverButtonInterraction(new ShowStringEventArgs("mouseOver"), new ShowStringEventArgs("MouseLeave"));
            mouseIsClickingButtonInterraction(new ShowStringEventArgs("mouseClick"), new ShowStringEventArgs("MouseRelease"));
        }

        #endregion
    }
}
