#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace PlatformerPrototype
{
    using Scene;
    using PuzzleEngineAlpha.Input;
    using PuzzleEngineAlpha.Resolution;
    using PuzzleEngineAlpha.Diagnostics;

    public class PlatformerPrototype : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameSceneDirector sceneDirector;
        ResolutionHandler resolutionHandler;
        public static bool Terminate = false;

        public PlatformerPrototype()
            : base()
        {
            this.graphics = new GraphicsDeviceManager(this)
             {
                 PreferMultiSampling = true,
                 PreferredBackBufferWidth = 1100,
                 PreferredBackBufferHeight = 650
             };
            graphics.PreparingDeviceSettings += OnPreparingDeviceSettings;

            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(OnWindowClientSizeChanged);

            // this.graphics.PreferMultiSampling = true;
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            this.graphics.ApplyChanges();

            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            resolutionHandler = new ResolutionHandler(ref this.graphics, false);
            InputHandler.Initialize();
            ConfigurationManager.Config = new Configuration();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneDirector = new GameSceneDirector(GraphicsDevice, Content,this.resolutionHandler);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Terminate)
                Exit();

            InputHandler.Update(gameTime);
            resolutionHandler.Update(gameTime);
            sceneDirector.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(new Color(20, 20, 20));

            sceneDirector.Draw(spriteBatch);

            base.Draw(gameTime);


        }

        private void OnWindowClientSizeChanged(object sender, System.EventArgs e)
        {
            this.resolutionHandler.SetResolution(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

        }

        private void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
    }
}