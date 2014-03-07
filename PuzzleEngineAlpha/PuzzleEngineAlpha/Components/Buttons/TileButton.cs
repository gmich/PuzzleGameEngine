using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Input;

namespace PuzzleEngineAlpha.Components.Buttons
{
    using Actions;
    using Camera;
    /// <summary>
    /// MenuButton responds to mouse input
    /// </summary>
    public class TileButton : AGUIComponent
    {

        #region Declarations

        protected DrawProperties button;
        protected DrawProperties frame;
        Camera camera;
        Rectangle sourceRectangle;

        #endregion

        #region Constructor

        public TileButton(DrawProperties buttonDrawProperties, DrawProperties frameDrawProperties, Vector2 position, Vector2 size,Rectangle sourceRectangle,Camera camera, Rectangle generalArea, int layertile)
            : base()
        {
            button = buttonDrawProperties;
            frame = frameDrawProperties;
            this.sourceRectangle = sourceRectangle;
            this.camera = camera;
            this.Size = size;
            this.Position = position;
            this.GeneralArea = generalArea;
            this.LayerTile = layertile;
        }

        #endregion
        
        #region Drawing Related Methods

        public int LayerTile
        {
            get;
            private set;
        }

        public Rectangle SourceRectangle
        {
            get
            {
                return sourceRectangle;
            }
            set
            {
                sourceRectangle = value;
            }
        }

        Vector2 position;
        public override Vector2 Position
        {
            get
            {
                return this.position - camera.Position;
            }
            set
            {
                position = value;
            }
        }

        #region Drawing Properties


        Rectangle ButtonRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        #endregion

        #endregion

        #region Mouse Response

        public override void IsClicking()
        {
            if (IsFocused && InputHandler.LeftButtonIsClicked())
            {
                OnMouseDown();
            }

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
            Vector2 CurrentMousePosition = Vector2.Transform(InputHandler.MousePosition,
                                    Matrix.Invert(camera.GetTransformation()));

            if (this.Intersects(CurrentMousePosition) && !IsFocused)
            {
                OnFocus();
                IsFocused = true;
            }
            else if (!this.Intersects(CurrentMousePosition) && IsFocused)
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
            if (canRelease && IsFocused)
            {
                DrawableEntity(spriteBatch, sourceRectangle,button,new Color(200, 200, 200));
                DrawableEntity(spriteBatch, new Rectangle(0, 0, frame.texture.Width, frame.texture.Height), frame, new Color(60, 60, 60));
            }
            else if (IsFocused)
            {
                DrawableEntity(spriteBatch, sourceRectangle, button, button.color);
                DrawableEntity(spriteBatch, new Rectangle(0, 0, frame.texture.Width, frame.texture.Height), frame, new Color(60, 60, 60));
            }
            else
                DrawableEntity(spriteBatch, sourceRectangle, button, button.color);
        }

        protected void DrawableEntity(SpriteBatch spriteBatch, Rectangle rect, DrawProperties entity, Color color)
        {
            spriteBatch.Draw(entity.texture, camera.WorldToScreen(ButtonRectangle), rect, color * entity.transparency, entity.rotation, Vector2.Zero, SpriteEffects.None, entity.layer);
        }

        #endregion

    }
}
