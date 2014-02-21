using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Game
{
    using Diagnostics;
    using Resolution;

    public class DiagnosticsScene : IScene
    {

        #region Declarations

        static Dictionary<Vector2, String> texts = new Dictionary<Vector2, string>();
        SpriteFont font;
        GraphicsDevice graphicsDevice;
        FpsMonitor fpsMonitor;

        #endregion

        #region Constructor

        public DiagnosticsScene(GraphicsDevice graphicsDevice, ContentManager content)
        {
            fpsMonitor = new FpsMonitor();
            this.graphicsDevice = graphicsDevice;
            this.font = content.Load<SpriteFont>(@"Fonts/font");
            texts = new Dictionary<Vector2, string>();
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

        #region Static Methods

        public static void AddText(Vector2 location, string text)
        {
            DiagnosticsScene.texts.Add(location, text);
        }

        public static void SetText(Vector2 location, string text)
        {
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
            SetText(new Vector2(10, 100), "FPS: " + fpsMonitor.FPS);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            fpsMonitor.AddFrame();
           
            
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend);

            foreach (KeyValuePair<Vector2, string> item in texts)
            {
                spriteBatch.DrawString(font, item.Value, item.Key, Color.Black);
            }


            spriteBatch.End();

        }

        #endregion

    }
}
