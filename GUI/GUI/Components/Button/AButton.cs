using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input;

namespace Button
{

    abstract class AButton : IEquatable<Vector2>
    {

        #region Constructor 

        //TODO: update to support more properties
        public AButton(Texture2D buttonTexture, Texture2D frameTexture, Texture2D clickedButtonTexture, SpriteFont font, Vector2 location, Vector2 size, Color textColor, string text)
        {
            button = new ButtonDrawFields(buttonTexture, 0.9f, 1.0f, 0.0f, Color.White);
            frame = new ButtonDrawFields(frameTexture, 0.8f, 1.0f, 0.0f, Color.White);
            clickedButton = new ButtonDrawFields(clickedButtonTexture, 0.8f, 1.0f, 0.0f, Color.White);
            Location = location;
            this.Text = text;
            this.Size = size;
            mouseIsOverButton = false;
            mouseIsClickingButton = false;
            mouseCanRelease = false;
            this.Font = font;
            this.TextColor = textColor;
            this.TextLayer = 0.7f;
            this.TextScale = 1.0f;
        }

        #endregion 

        #region Button To Screen Info

        #region Fields

        Vector2 location;
        Vector2 size;

        #endregion

        //Override if the position is not static
        public virtual Vector2 Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        #endregion

        #region Mouse Events

        protected virtual void OnMouseOver(EventArgs e)
        {
            ;
        }

        protected virtual void OnMouseLeave(EventArgs e)
        {
            ;
        }
        protected virtual void OnMouseClick(EventArgs e)
        {
            ;
        }

        protected virtual void OnMouseRelease(EventArgs e)
        {
            ;
        }

        #endregion

        #region Implement IEquatable

        public bool Equals(Vector2 otherLocation)
        {
            return ((Location.X <= otherLocation.X && Location.Y <= otherLocation.Y)
                    && (Location.X + size.X >= otherLocation.X && Location.Y + size.Y >= otherLocation.Y));
        }

        #endregion

        #region Mouse to Button Interraction

        bool mouseIsOverButton;
        protected virtual void mouseIsOverButtonInterraction(EventArgs mouseOverArgs, EventArgs mouseLeaveArgs)
        {
            if (this.Equals(InputManager.MousePosition) && !mouseIsOverButton)
            {
                OnMouseOver(mouseOverArgs);
                mouseIsOverButton = true;
            }
            else if (!this.Equals(InputManager.MousePosition) && mouseIsOverButton)
            {
                OnMouseLeave(mouseLeaveArgs);
                mouseIsOverButton = false;
            }
        }

        protected bool mouseIsClickingButton;
        protected bool mouseCanRelease;
        protected virtual void mouseIsClickingButtonInterraction(EventArgs mouseClickArgs, EventArgs mouseReleaseArgs)
        {
            if (mouseIsOverButton && InputManager.LeftButtonIsClicked() && !mouseIsClickingButton)
            {
                OnMouseClick(mouseClickArgs);
                mouseIsClickingButton = true;
                mouseCanRelease = true;
            }
            else if (InputManager.LeftButtonIsClicked() && !mouseIsOverButton)
            {
                mouseIsClickingButton = true;
            }
            else if (!InputManager.LeftButtonIsClicked())
            {
                mouseIsClickingButton = false;
                if (mouseCanRelease && mouseIsOverButton)
                    OnMouseRelease(mouseReleaseArgs);
                mouseCanRelease = false;
            }
        }
        
        #endregion

        #region Update

        public virtual void Update(GameTime gameTime)
        {
            ;
        }

        #endregion

        #region Draw & Drawing Information

        #region Drawing Properties

        Rectangle ButtonRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public struct ButtonDrawFields
        {
            public Texture2D texture;
            public float layer;
            public float transparency;
            public float rotation;
            public Color color;
            public ButtonDrawFields(Texture2D texture, float layer, float transparency, float rotation, Color color)
            {
                this.texture = texture;
                this.layer = layer;
                this.transparency = transparency;
                this.rotation = rotation;
                this.color = color;
            }
        }

        protected ButtonDrawFields button;
        protected ButtonDrawFields frame;
        protected ButtonDrawFields clickedButton;

        #endregion

        #region Text

        public string Text
        {
            get;
            set;
        }

        protected virtual Vector2 TextLocation
        {
            get
            {
                return new Vector2(Location.X + (button.texture.Width / 2) - (FontSize.X / 2), Location.Y + (button.texture.Height / 2) - FontSize.Y);
            }
        }

        Vector2 FontSize
        {
            get
            {
                return Font.MeasureString(Text);
            }
        }

        float TextLayer
        {
            get;
            set;
        }

        float TextScale
        {
            get;
            set;
        }

        Color TextColor
        {
            get;
            set;
        }

        SpriteFont Font
        {
            get;
            set;
        }

        #endregion

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawableEntity(spriteBatch, ref button);

            if (mouseCanRelease && mouseIsOverButton)
            {
                DrawableEntity(spriteBatch, ref clickedButton);
            }
            if (mouseIsOverButton)
                DrawableEntity(spriteBatch, ref frame);

            spriteBatch.DrawString(Font, Text, TextLocation, TextColor, 0.0f, Vector2.Zero, TextScale, SpriteEffects.None, TextLayer);
        }

        void DrawableEntity(SpriteBatch spriteBatch, ref ButtonDrawFields entity)
        {
            spriteBatch.Draw(entity.texture, ButtonRectangle, null, entity.color * entity.transparency, entity.rotation, Vector2.Zero, SpriteEffects.None, entity.layer);
        }

        #endregion
    }
}