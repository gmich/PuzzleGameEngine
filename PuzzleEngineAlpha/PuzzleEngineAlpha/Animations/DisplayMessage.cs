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

        #region Constructor

        public DisplayMessage(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"Fonts/menuButtonFont");
            background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            transition=new SmoothTransition(0.0f,0.002f,0.0f,1.0f);
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
                return new Vector2(Resolution.ResolutionHandler.WindowWidth/2 - MessageSize.X/2, Resolution.ResolutionHandler.WindowHeight/2 - MessageSize.Y/2-150);
            }
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
                spriteBatch.DrawString(font, Message, MessageLocation, Color.Black * transition.Value,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,0.95f);
                spriteBatch.Draw(background, BackgroundRectangle,null, Color.White * transition.Value,0.0f,Vector2.Zero,SpriteEffects.None,0.96f);
                spriteBatch.Draw(background, FrameRectangle,null, Color.Black * transition.Value, 0.0f, Vector2.Zero, SpriteEffects.None, 0.97f);
 

            }
        }
        
        #endregion
    }
}
