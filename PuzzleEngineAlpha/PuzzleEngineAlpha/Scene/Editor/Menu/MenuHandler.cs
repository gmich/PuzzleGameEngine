using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    public class MenuHandler : IScene
    {

        #region Declarations

        MenuStateEnum currentState;
        Animations.SmoothTransition transition;
        Camera.Camera camera;
        GraphicsDevice graphicsDevice;
        Dictionary<string, IScene> menuWindows;
        IScene activeWindow;
        IScene pendingWindow;

        #endregion

        #region Constructor

        public MenuHandler(ContentManager Content, GraphicsDevice graphicsDevice, Editor.MapHandlerScene mapHandler,Level.TileMap tileMap)
        {
            this.graphicsDevice = graphicsDevice;
            menuWindows = new Dictionary<string, IScene>();
            menuWindows.Add("mainMenu", new MainMenu(Content,this));
            menuWindows.Add("newMap", new NewMapMenu(Content,this));
            menuWindows.Add("loadMap", new LoadMapMenu(graphicsDevice,Content, this,mapHandler,tileMap));
            menuWindows.Add("saveMap", new SaveMapMenu(Content, this,mapHandler));
            menuWindows.Add("settings", new SettingsMenu(Content, this));

            activeWindow = menuWindows["mainMenu"];
            IsActive = false;
            currentState = new MenuStateEnum();
            currentState = MenuStateEnum.Hidden;
            transition = new Animations.SmoothTransition(0.0f, 0.011f, 0.0f, 1.0f);

            this.camera = new Camera.Camera(Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight), new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight));
            camera.Zoom = transition.Value;
            Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion
        
        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            this.camera = new Camera.Camera(Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight), new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight));
            camera.Zoom = transition.Value;
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

                if (isActive)
                {
                    activeWindow = menuWindows["mainMenu"];
                    currentState = MenuStateEnum.Maximizing;
                }
            }
        }

        #region Helper Methods
        
        public void SwapWindow(string window)
        {
            if (menuWindows.ContainsKey(window))
            {
                pendingWindow = menuWindows[window];
                this.currentState = MenuStateEnum.Minimizing;
            }
        }

        public void GoInactive()
        {
            currentState = MenuStateEnum.Minimizing;
            pendingWindow = null;
        }

        #endregion

        #region Private State Manipulation Methods

        void ManipulateScale(GameTime gameTime)
        {
            switch (currentState)
            {
                case MenuStateEnum.Maximizing:
                    transition.Increase(gameTime);
                    break;
                case MenuStateEnum.Minimizing:
                    transition.Decrease(gameTime);
                    break;
            }
            if (transition.Value == 0.0f)
            {
                if (pendingWindow == null)
                    currentState = MenuStateEnum.Hidden;
                else
                {
                    activeWindow = pendingWindow;
                    activeWindow.IsActive = true;
                    this.currentState = MenuStateEnum.Maximizing;
                }
            }
            else if (transition.Value == transition.MaxValue)
            {
                currentState = MenuStateEnum.Focused;
            }
            camera.Zoom = transition.Value;
        }

        #endregion

        #region Properties

        MenuStateEnum State
        {
            get
            {
                return currentState;
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            ManipulateScale(gameTime);

            if (this.State == MenuStateEnum.Hidden)
            {
                this.IsActive = false;
            }

            if (this.State == MenuStateEnum.Focused)
            {
                activeWindow.Update(gameTime);
            }
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront,
            BlendState.AlphaBlend,
            SamplerState.PointWrap,
            null,
            null,
            null,
            camera.GetTransformation());

            activeWindow.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
