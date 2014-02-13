using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GUI.Input;

namespace GUI.Components.Buttons
{
    using Actions;

    public class MenuButton : AGUIElement
    {

        #region Declarations

        DrawProperties button;
        DrawProperties frame;
        DrawProperties clickedButton;
        DrawTextProperties defaultText;

        #endregion

        #region Constructor

        public MenuButton(DrawProperties buttonDrawProperties, DrawProperties frameDrawProperties, DrawProperties clickedButtonDrawProperties, DrawTextProperties textProperties, Vector2 position, Vector2 size)
            : base()
        {
            button = buttonDrawProperties;
            frame = frameDrawProperties;
            clickedButton = clickedButtonDrawProperties;
            defaultText = textProperties;
            this.Size = size;
            this.Position = position;
        }

        #endregion
        
        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #endregion

        #region Drawing Related Methods

        #region Drawing Properties

        Rectangle ButtonRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }


        #endregion

        #region TextProperties

        Vector2 TextLocation
        {
            get
            {
                return new Vector2(Position.X + (button.texture.Width / 2) - (FontSize.X / 2), Position.Y + (button.texture.Height / 2) - FontSize.Y);
            }
        }

        Vector2 FontSize
        {
            get
            {
                return defaultText.font.MeasureString(defaultText.text);
            }
        }

        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawableEntity(spriteBatch, button);

            if (mouseCanRelease && mouseIsOverButton)
            {
                DrawableEntity(spriteBatch, clickedButton);
            }
            if (mouseIsOverButton)
                DrawableEntity(spriteBatch, frame);

            spriteBatch.DrawString(defaultText.font, defaultText.text, TextLocation, defaultText.textColor, 0.0f, Vector2.Zero, defaultText.textScale, SpriteEffects.None, defaultText.textLayer);
        }

        void DrawableEntity(SpriteBatch spriteBatch, DrawProperties entity)
        {
            spriteBatch.Draw(entity.texture, ButtonRectangle, null, entity.color * entity.transparency, entity.rotation, Vector2.Zero, SpriteEffects.None, entity.layer);
        }

        #endregion

    }
}
