using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Input;

namespace PuzzleEngineAlpha.Components.Buttons
{
    using Actions;

    /// <summary>
    /// MenuButton responds to mouse input
    /// </summary>
    public class MenuButton : AGUIComponent
    {

        #region Declarations

        DrawProperties button;
        DrawProperties frame;
        DrawProperties clickedButton;
        DrawTextProperties defaultText;

        #endregion

        #region Constructor

        public MenuButton(DrawProperties buttonDrawProperties, DrawProperties frameDrawProperties, DrawProperties clickedButtonDrawProperties, DrawTextProperties textProperties, Vector2 position, Vector2 size,Rectangle generalArea)
            : base()
        {
            button = buttonDrawProperties;
            frame = frameDrawProperties;
            clickedButton = clickedButtonDrawProperties;
            defaultText = textProperties;
            this.Size = size;
            this.Position = position;
            this.GeneralArea = generalArea;
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

        #endregion

        #region TextProperties

        Vector2 TextLocation
        {
            get
            {
                return new Vector2(Position.X + (Size.X / 2) - (FontSize.X / 2), Position.Y + (Size.Y / 2) - FontSize.Y/2);
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

        #region Mouse Response

        public override void IsClicking()
        {
            if (IsFocused && InputHandler.LeftButtonIsClicked() && !isClicking)
            {
                OnClick();
                isClicking = true;
                canRelease = true;
            }
            else if (InputHandler.LeftButtonIsClicked() && !IsFocused)
            {
                isClicking = true;
            }
            else if (!InputHandler.LeftButtonIsClicked())
            {
                isClicking = false;
                if (canRelease && IsFocused)
                    OnRelease();
                canRelease = false;
            }
        }

        void mouseIsOver()
        {
            if (!this.GeneralArea.Intersects(InputHandler.MouseRectangle))
            {
                IsFocused = false;
                return;
            }

            if (this.Intersects(InputHandler.MousePosition) && !IsFocused)
            {
                OnFocus();
                IsFocused = true;
            }
            else if (!this.Intersects(InputHandler.MousePosition) && IsFocused)
            {
                OnFocusLeave();
                IsFocused = false;
            }
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            mouseIsOver();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawableEntity(spriteBatch, button);

            if (canRelease && IsFocused)
            {
                DrawableEntity(spriteBatch, clickedButton);
            }
            else if (IsFocused)
            {
                DrawableEntity(spriteBatch, frame);
            }
            spriteBatch.DrawString(defaultText.font, defaultText.text, TextLocation, defaultText.textColor, 0.0f, Vector2.Zero, defaultText.textScale, SpriteEffects.None, defaultText.textLayer);
        }

        void DrawableEntity(SpriteBatch spriteBatch, DrawProperties entity)
        {
            spriteBatch.Draw(entity.texture, ButtonRectangle, null, entity.color * entity.transparency, entity.rotation, Vector2.Zero, SpriteEffects.None, entity.layer);
        }

        #endregion

    }
}
