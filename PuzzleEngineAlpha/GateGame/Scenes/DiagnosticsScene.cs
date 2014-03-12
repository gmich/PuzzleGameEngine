using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Scene;
using PuzzleEngineAlpha.Diagnostics;
using PuzzleEngineAlpha.Resolution;
using PuzzleEngineAlpha.Animations;

namespace GateGame.Scene
{

    public class DiagnosticsScene : IScene
    {

        #region Declarations

        static Dictionary<Vector2, String> texts = new Dictionary<Vector2, string>();
        static SpriteFont font;
        GraphicsDevice graphicsDevice;
        FpsMonitor fpsMonitor;
        Texture2D background;
        SmoothTransition bgTransparency;
        SmoothTransition fontTransparency;

        #endregion

        #region Constructor

        public DiagnosticsScene(GraphicsDevice graphicsDevice, ContentManager content)
        {
            fpsMonitor = new FpsMonitor();
            this.graphicsDevice = graphicsDevice;
            font = content.Load<SpriteFont>(@"Fonts/diagnosticsFont");
            this.background = content.Load<Texture2D>(@"Textures/whiteRectangle");
            texts = new Dictionary<Vector2, string>();
            LargestWidth = 0;
            LargestHeight = 0;
            bgTransparency = new SmoothTransition(0.5f, 0.002f, 0.0f, 0.5f);
            fontTransparency = new SmoothTransition(1.0f, 0.004f, 0.0f, 1.0f);
            isActive = true;
        }

        #endregion

        #region Properties


        Vector2 SceneLocation
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public static int LargestWidth
        {
            get;
            set;
        }

        public static int LargestHeight
        {
            get;
            set;
        }

        Rectangle SceneRectangle
        {
            get
            {
                return new Rectangle(0, 0, LargestWidth + 10, LargestHeight + 10);
            }
        }

        #region Boundaries

        int Width
        {
            get
            {
                return ResolutionHandler.WindowWidth;
            }
        }

        int Height
        {
            get
            {
                return ResolutionHandler.WindowHeight;
            }
        }

        #endregion

        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
        
        #endregion

        #region Helper Methods

        public void GoInactive()
        {
            this.IsActive = false;
        }

        static int StringScreenWidth(string stringToMeasure)
        {
            return (int)font.MeasureString(stringToMeasure).X;
        }

        static int StringScreenHeight(string stringToMeasure)
        {
            return (int)font.MeasureString(stringToMeasure).Y;
        }

        #endregion

        #region Static Methods

        public static void AddText(Vector2 location, string text)
        {
            if (!texts.ContainsKey(location))
                DiagnosticsScene.texts.Add(location, text);
        }

        public static void SetText(Vector2 location, string text)
        {
            if ((int)location.X + StringScreenWidth(text) > LargestWidth)
                LargestWidth = (int)location.X + StringScreenWidth(text);

            if ((int)location.Y + StringScreenHeight(text) > LargestHeight)
                LargestHeight = (int)location.Y + StringScreenHeight(text);

            DiagnosticsScene.texts[location] = text;
        }

        #endregion

        #region RenderTarget Stub

        public void UpdateRenderTarget()
        {
            return;
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            fpsMonitor.Update(gameTime);
            SetText(new Vector2(5,5), "fps: " + fpsMonitor.FPS);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            fpsMonitor.AddFrame();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                      
            spriteBatch.Draw(background, SceneRectangle, null, Color.White * bgTransparency.Value, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

            foreach (KeyValuePair<Vector2, string> item in texts)
            {
                spriteBatch.DrawString(font, item.Value, item.Key, Color.Black * fontTransparency.Value, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
            
            spriteBatch.End();

        }

        #endregion

    }
}
