using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level.Editor
{
    using Components.Buttons;
    using Components;

    public class EditorMapSquare : TileButton
    {
        #region Declarations

        DrawTextProperties passableText;
        DrawTextProperties codeValueText;
        Texture2D background;
        DrawProperties buttonActor;

        #endregion

        #region Public Constructor

        public EditorMapSquare(DrawTextProperties passableText, DrawTextProperties codeValueText, Texture2D background, DrawProperties buttonDrawProperties,DrawProperties buttonActorDrawProperties, DrawProperties frameDrawProperties, Vector2 position, Vector2 size, Rectangle sourceRectangle, Camera.Camera camera, Rectangle generalArea, int layerTile)
            : base(buttonDrawProperties, frameDrawProperties, position, size, sourceRectangle, camera, generalArea, layerTile)
        {
            this.passableText = passableText;
            this.codeValueText = codeValueText;
            this.background = background;
            this.buttonActor = buttonActorDrawProperties;
        }

        #endregion

        #region TextProperties

        Vector2 TextLocation(Vector2 fontSize)
        {
            return new Vector2(Position.X + (Size.X / 2) - (fontSize.X / 2), Position.Y + (Size.Y / 2) - fontSize.Y / 2);      
        }

        Vector2 StringToScreenSize(string text)
        {
                return passableText.font.MeasureString(text);
        }

        Rectangle PassableTextRectangle
        {
            get
            {
                Vector2 fontSize = StringToScreenSize(passableText.text);
                Vector2 textLocation = TextLocation(fontSize);
                return new Rectangle((int)textLocation.X, (int)textLocation.Y + 6, (int)fontSize.X, (int)fontSize.Y - 12);

            }
        }

        Rectangle CodeValueTextRectangle
        {
            get
            {
                Vector2 fontSize = StringToScreenSize(MapSquare.CodeValue);
                Vector2 textLocation = TextLocation(fontSize);
                return new Rectangle((int)textLocation.X, (int)textLocation.Y-15, (int)fontSize.X, (int)fontSize.Y-10 );

            }
        }

        #endregion

        public MapSquare MapSquare
        {
            get;
            set;
        }

        public Rectangle ActorSourceRectangle
        {
            get;
            set;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (MapSquare.ActorID > -1)
            {
                if (IsFocused)
                    DrawableEntity(spriteBatch, ActorSourceRectangle, buttonActor, new Color(225, 225, 225));
                else
                    DrawableEntity(spriteBatch, ActorSourceRectangle, buttonActor, buttonActor.color);
            }
            if (!MapSquare.Passable && TileManager.ShowPassable)
            {
                spriteBatch.Draw(background, PassableTextRectangle, null, Color.White * 0.4f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(passableText.font, passableText.text, TextLocation(StringToScreenSize(passableText.text)), passableText.textColor, 0.0f, Vector2.Zero, passableText.textScale, SpriteEffects.None, passableText.textLayer);

            }
            if (TileManager.ShowPassable)
            {
                if (MapSquare.CodeValue != null && MapSquare.CodeValue!="")
                {
                    spriteBatch.Draw(background, CodeValueTextRectangle, null, Color.White * 0.8f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.8f);
                    spriteBatch.DrawString(codeValueText.font, MapSquare.CodeValue, TextLocation(StringToScreenSize(MapSquare.CodeValue))- new Vector2(0,+20), passableText.textColor, 0.0f, Vector2.Zero, passableText.textScale, SpriteEffects.None, passableText.textLayer);
                }
            }
        }
    }

}

