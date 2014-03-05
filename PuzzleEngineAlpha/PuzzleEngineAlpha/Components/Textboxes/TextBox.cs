using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Components.TextBoxes
{
    using Input;
    using Actions;
    public class TextBox
    {

        #region Declarations

        private KeyboardInput keyboardManager;
        private Vector2 initialPos;
        private float timePassed;
        private bool MouseOver;
        private float Transparency;
        private bool lastActive;
        private List<IAction> textChange;
        private float layer;

        #endregion

        #region Constructor

        public TextBox(KeyboardInput keyboardInputType,SpriteFont font,Texture2D texture,Vector2 location,int width,int height,float layer)
        {
            Font = font;
            timePassed = 0;
            Texture = texture;
            Location = location;
            Width = width;
            Height = height;
            InitialPosition = location;
            keyboardManager = keyboardInputType;
            lastActive = Active = ShowPrompt = false;
            TextColor = Color.Black;
            AlphaChangeRate=0.04f;
            textChange = new List<IAction>();
            this.layer = layer;

        }

        #endregion

        #region Custom Events

        protected void OnTextChange()
        {
            foreach (IAction action in textChange)
                action.Execute();
        }

        public void StoreAndExecuteOnTextChange(IAction action)
        {
            textChange.Add(action);
        }

        #endregion

        #region Properties

        #region Effects Properties

        private float AlphaChangeRate
        {
            get;
            set;
        }

        #endregion

        public Color TextColor
        {
            get;
            set;
        }
        private bool Active
        {
            get;
            set;
        }

        private bool ShowPrompt
        {
            get;
            set;
        }

        private SpriteFont Font
        {
            get;
            set;
        }

        private Vector2 PromptLocation
        {
            get
            {
                Vector2 stringSize=Font.MeasureString(keyboardManager.Text);
                return new Vector2(Location.X + stringSize.X, Location.Y );
            }
        }

        private Texture2D Texture
        {
            get;
            set;
        }

        private Vector2 InitialPosition
        {
            get
            {
                return new Vector2(initialPos.X + 5, initialPos.Y + Height / 2);
            }
            set
            {
                initialPos = value;
            }
        }

        public Vector2 Location
        {
            get;
            set;
        }

        private int Width
        {
            get;
            set;
        }

        private int Height
        {
            get;
            set;
        }

        private Rectangle LayoutRectangle
        {
            get { return new Rectangle((int)Location.X, (int)Location.Y, Width, Height); }
        }

        public string Text
        {
            get
            {
                return keyboardManager.Text;
            }
            set
            {     
                keyboardManager.Text = value;
            }
        }

        #endregion

        #region Layout Activity

        public void LayoutActive()
        {
            if (InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                MouseOver = false;
                Active = false;
                return;
            }

            if (LayoutRectangle.Intersects(InputHandler.MouseRectangle))
            {
                MouseOver=true;
                if (InputHandler.LeftButtonIsClicked())
                Active = true;
            }
            else if (!LayoutRectangle.Intersects(InputHandler.MouseRectangle))
            {
                MouseOver=false;
                if (InputHandler.LeftButtonIsClicked())
                Active = false;
            }

        }

        #endregion

        #region Prompt Animation

        public void AnimatePrompt()
        {
            if (timePassed <300)
                ShowPrompt = true;
            else
                ShowPrompt = false;

            if (timePassed >= 600)
                timePassed = 0;
        }

        #endregion

        #region Effects

        public void UpdateTransparency()
        {
            if (Active)
            {
                Transparency = MathHelper.Min(1.0f, Transparency + AlphaChangeRate * 2);
                return;
            }

            if (MouseOver)
            {
                if (Transparency > 0.6)
                    Transparency = MathHelper.Max(0.7f, Transparency - AlphaChangeRate);
                else
                    Transparency = MathHelper.Min(0.7f, Transparency + AlphaChangeRate);
            }
            else
            {
                Transparency = MathHelper.Max(0.5f, Transparency - AlphaChangeRate);
            }
        }

        #endregion

        #region Update

        public bool IsTextBoxUpdated()
        {        
            return (lastActive != Active && Active == false);
        }

        public void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.Milliseconds;

            lastActive = Active;

            AnimatePrompt();
            LayoutActive();
            UpdateTransparency();

            keyboardManager.BufferFull = (PromptLocation.X > Location.X + Width-20 );

            if (Active)
            {
                keyboardManager.Update(gameTime);
            }

            if(IsTextBoxUpdated()) OnTextChange();
            
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, LayoutRectangle, null,Color.White*Transparency,0.0f,Vector2.Zero,SpriteEffects.None,layer);
            spriteBatch.DrawString(Font, keyboardManager.Text, Location, TextColor,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,layer+0.01f);

            if(ShowPrompt && Active)
                spriteBatch.DrawString(Font, "_", PromptLocation, TextColor, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, layer + 0.01f);
        }

        #endregion
    }
}
