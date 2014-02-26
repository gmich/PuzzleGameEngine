using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Game
{
    using Input;
    using Components;
    using GUIManager;
    using Resolution;

    public class MainMenu : IScene
    {
        #region Declarations

       // static ICameraScript cameraScript;
        RenderTarget2D renderTarget;
        GraphicsDevice graphicsDevice;
        GUIHandler guiHandler;

        #endregion

        #region Constructor

        public MainMenu(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
             guiHandler = new GUIHandler(content);
          
            UpdateRenderTarget();
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
                return ResolutionHandler.WindowHeight ;
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

        public void UpdateRenderTarget()
        {
            renderTarget = new RenderTarget2D(graphicsDevice, this.Width, this.Height);

        }

        public void GoInactive()
        {
            this.IsActive = false;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            guiHandler.Update(gameTime);
        }

        //TODO: fix rendertarget order
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            graphicsDevice.SetRenderTarget(renderTarget);


            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend);

            guiHandler.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            spriteBatch.End();
            
        }
    }
}
