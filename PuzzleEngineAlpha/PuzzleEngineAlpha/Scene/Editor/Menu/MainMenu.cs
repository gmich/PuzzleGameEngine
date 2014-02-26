using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;

    class MainMenu : IMenuWindow
    {

        #region Declarations

        MenuStateEnum currentState;
        Animations.SmoothTransaction transaction;
        List<AGUIComponent> components;
        RenderTarget2D renderTarget;
        Camera.Camera camera;
        GraphicsDevice graphicsDevice;

        #endregion

        #region Constructor

        public MainMenu(ContentManager Content,GraphicsDevice graphicsDevice,Vector2 size,Vector2 location )
        {
            this.Size = size;
            this.graphicsDevice = graphicsDevice;
            this.Location = location;
            currentState = new MenuStateEnum();
            currentState = MenuStateEnum.Hidden;
            transaction = new Animations.SmoothTransaction(0.0f, 0.01f, 0.0f, 1.0f);
            components = new List<AGUIComponent>();
            this.camera = new Camera.Camera(Vector2.Zero, size, size);
            renderTarget = new RenderTarget2D(graphicsDevice, (int)this.Size.X, (int)this.Size.Y);
            InitializeGUI(Content);
        }

        #endregion

        #region Public State Manipulation Methods

        public void Show()
        {
            currentState = MenuStateEnum.Maximizing;
        }

        public void Hide()
        {
            currentState = MenuStateEnum.Minimizing;
        }

        #endregion

        #region Private State Manipulation Methods

        void ManipulateScale(GameTime gameTime)
        {           
            switch (currentState)
            {
                case MenuStateEnum.Maximizing:
                    transaction.Increase(gameTime);
                    break;
                case MenuStateEnum.Minimizing:
                    transaction.Decrease(gameTime);
                    break;
            }
            if (transaction.Value == 0.0f)
            {
                currentState = MenuStateEnum.Hidden;
            }
            else if (transaction.Value == transaction.MaxValue)
            {
                currentState = MenuStateEnum.Focused;
            }
            camera.Zoom = transaction.Value;
        }

        #endregion

        #region Properties

        Vector2 Size
        {
            get;
            set;
        }

        Vector2 Location
        {
            get;
            set;
        }

        Rectangle MenuRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public bool IsFocused
        {
            get { return (currentState == MenuStateEnum.Focused); }
        }

        public bool IsShown
        {
            get
            {
                return (currentState == MenuStateEnum.Focused || currentState == MenuStateEnum.Maximizing);
            }
        }

        #endregion

        #region Initialize GUI

        void InitializeGUI(ContentManager Content)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("new map", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, new Vector2(5, 50) + Location, new Vector2(160, 40), this.MenuRectangle));
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            ManipulateScale(gameTime);

            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
       
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.PointWrap,
                        null,
                        null,
                        null,
                        camera.GetTransformation());

            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, MenuRectangle,null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            spriteBatch.End();
        }
    }
}
