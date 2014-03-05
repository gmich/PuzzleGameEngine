using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Animations
{
    public class DisplayMessage
    {
        #region Declarations

        SpriteFont font;
        Texture2D background;
        SmoothTransition transition;
        float timePassed;

        #endregion

        #region Constructors

        public DisplayMessage(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"Fonts/menuButtonFont");
            background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            transition=new SmoothTransition(0.0f,0.002f,0.0f,1.0f);
            OffSet = Vector2.Zero;
        }

        public DisplayMessage(ContentManager Content,Vector2 offSet,string message,int duration)
        {
            font = Content.Load<SpriteFont>(@"Fonts/menuButtonFont");
            background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            transition = new SmoothTransition(0.0f, 0.002f, 0.0f, 1.0f);
            OffSet = Vector2.Zero;
            this.OffSet = offSet;
            this.StartAnimation(message, duration);
        }
        
        #endregion

        #region Public Animation Related Properties

        public string Message
        {
            get;
            set;
        }

        public float Duration
        {
            get;
            set;
        }

        public bool IsAlive
        {
            get
            {
                return transition.Value>transition.MinValue;
            }
        }

        #region DrawingProperties

        Rectangle FrameRectangle
        {
            get
            {
                int offSet = 3;
                return new Rectangle((int)MessageLocation.X - offSet, (int)MessageLocation.Y - offSet, (int)MessageSize.X + offSet*2, (int)MessageSize.Y + offSet*2);
            }
        }

        Rectangle BackgroundRectangle
        {
            get
            {
                int offSet = 2;
                return new Rectangle((int)MessageLocation.X - offSet, (int)MessageLocation.Y - offSet, (int)MessageSize.X + offSet*2, (int)MessageSize.Y + offSet*2);
            }
        }

        Vector2 MessageSize
        {
            get
            {
                return font.MeasureString(Message);
            }
        }

        Vector2 MessageLocation
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - MessageSize.X / 2 - OffSet.X, Resolution.ResolutionHandler.WindowHeight / 2 - MessageSize.Y / 2 - OffSet.Y);
            }
        }

        public Vector2 OffSet
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Animation Methods

        public void StartAnimation(string message,float duration)
        {
            this.Message = message;
            this.Duration = duration;
            this.transition.Reset();
            timePassed = 0.0f;

        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (this.IsAlive)
            {
                if (Duration < 0.0f) return;

                timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timePassed > Duration)
                {
                    transition.Decrease(gameTime);
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsAlive)
            {
                spriteBatch.Draw(background, FrameRectangle, null, Color.Black * transition.Value, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MessageBox + 0.01f);
                spriteBatch.Draw(background, BackgroundRectangle, null, Color.White * transition.Value, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MessageBox + 0.01f); 
                spriteBatch.DrawString(font, Message, MessageLocation, Color.Black * transition.Value, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Scene.DisplayLayer.MessageBox);
            }
        }
        
        #endregion
    }
}
