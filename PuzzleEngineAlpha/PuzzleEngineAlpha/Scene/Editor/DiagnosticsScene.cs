using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Editor
{
    using Diagnostics;
    using Resolution;

    public class DiagnosticsScene : IScene
    {

        #region Declarations

        static Dictionary<Vector2, String> texts = new Dictionary<Vector2, string>();
        static SpriteFont font;
        GraphicsDevice graphicsDevice;
        FpsMonitor fpsMonitor;
        Texture2D background;
        Animations.SmoothTransition bgTransparency;
        Animations.SmoothTransition fontTransparency;

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
            SelectedTexture = null;
            bgTransparency = new Animations.SmoothTransition(0.5f, 0.002f, 0.0f, 0.5f);
            fontTransparency = new Animations.SmoothTransition(1.0f, 0.004f, 0.0f, 1.0f);
            Level.Editor.TileManager.ShowPassable = true;
            isActive = true;
        }

        #endregion

        #region Private Helper Methods

        void UpdateTransparency(GameTime gameTime)
        {
            if (Input.InputHandler.MouseRectangle.Intersects(this.SceneRectangle))
            {
                bgTransparency.Decrease(gameTime);
                fontTransparency.Decrease(gameTime);
            }
            else
            {
                bgTransparency.Increase(gameTime);
                fontTransparency.Increase(gameTime);
            }
        }

        #endregion

        #region Properties

        public static Texture2D SelectedTexture
        {
            get;
            set;
        }

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
                return new Rectangle(0, 0, LargestWidth + 30, LargestHeight + 10);
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
            UpdateTransparency(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            fpsMonitor.AddFrame();

            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend);
                      
            spriteBatch.Draw(background, SceneRectangle, null, Color.White * bgTransparency.Value, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Diagnostics -0.01f);

            if (Level.Editor.TileManager.MapSquare != null)
            {
                spriteBatch.Draw(background, new Rectangle(142, 62, 70, 70), Level.Editor.TileManager.SourceRectangle, Color.Black * fontTransparency.Value, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Diagnostics);
                spriteBatch.Draw(Level.Editor.TileManager.TileSheet, new Rectangle(145, 65, 64, 64), Level.Editor.TileManager.SourceRectangle, Color.White * fontTransparency.Value, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Diagnostics + 0.01f);
            }

            foreach (KeyValuePair<Vector2, string> item in texts)
            {
                spriteBatch.DrawString(font, item.Value, item.Key, Color.Black * fontTransparency.Value,0.0f,Vector2.Zero,1.0f,SpriteEffects.None,Scene.DisplayLayer.Diagnostics +0.02f);
            }
            
            spriteBatch.End();

        }

        #endregion

    }
}
