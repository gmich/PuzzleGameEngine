using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Animations
{
    using Components.Titles;

    public class CircularAnimation : IAnimation
    {

        #region Declarations

        SpriteFont font;
        Vector2 location;
        float timePassed;

        #endregion

        #region Constructor

        public CircularAnimation(SpriteFont font,float radius)
        {
            this.font = font;
            Velocity = new Vector2(1, 1);
            this.Radius = radius;
            text = "...";
        }

        #endregion

        #region Properties

        public bool IsAlive
        {
            get
            {
                return true;
            }
        }

        string text;
        public string Text
        {
            set
            {
                text = value;
            }
            get
            {
                return text;
            }
        }

        public Vector2 Location
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - Radius / 2, Resolution.ResolutionHandler.WindowHeight / 2 -35- Radius / 2);
            }
            set
            {
                location = value;
            }
        }

        private Vector2 Velocity
        {
            get;
            set;
        }

        private float Step
        {
            get
            {
                return 200f;
            }
        }

        private float Radius
        {
            get;
            set;
        }

        private Vector2 PolarToCartesianConversion(float theta)
        {
            return new Vector2((float)Math.Cos(theta),(float)Math.Sin(theta));  
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            timePassed += gameTime.ElapsedGameTime.Milliseconds * 0.008f;
            Velocity = PolarToCartesianConversion(timePassed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(font, Text, Location+(Velocity*Radius), Color.Black,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,Scene.DisplayLayer.Animation);
        }
    }
}
