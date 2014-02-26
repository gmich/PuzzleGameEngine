﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Game
{
    using Resolution;
    public class GameBanner : IScene
    {
        #region Declarations

        //PuzzleEngineAlpha.Camera.Camera camera;
        RenderTarget2D renderTarget;
        bool isActive;

        #endregion

        #region Constructor

        public GameBanner(GraphicsDevice graphicsDevice)
        {
            renderTarget = new RenderTarget2D(graphicsDevice, ResolutionHandler.WindowWidth, Height);
        }

        #endregion

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

        public void GoInactive()
        {
            this.IsActive=false;
        }

        public static int Height
        {
            get
            {
                return ResolutionHandler.WindowHeight / 10;
            }

        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
