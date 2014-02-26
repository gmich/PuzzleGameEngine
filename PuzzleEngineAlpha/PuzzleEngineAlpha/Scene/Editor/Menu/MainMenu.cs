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
        Camera.Camera camera;
        GraphicsDevice graphicsDevice;
        Texture2D backGround;

        #endregion

        #region Constructor

        public MainMenu(ContentManager Content,GraphicsDevice graphicsDevice)
        {
            this.backGround = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            this.graphicsDevice = graphicsDevice;
            currentState = new MenuStateEnum();
            currentState = MenuStateEnum.Hidden;
            transaction = new Animations.SmoothTransaction(0.0f, 0.01f, 0.0f, 1.0f);
            components = new List<AGUIComponent>();
            InitializeGUI(Content);

            Resolution.ResolutionHandler.Changed += ResetSizes;
            this.camera = new Camera.Camera(Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight), new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight));
            camera.Zoom = transaction.Value;
        }

        #endregion

        #region Handle Resolution Change

        public void ResetSizes(object sender, EventArgs e)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * i);
                components[i].GeneralArea = this.MenuRectangle;
            }

            this.camera = new Camera.Camera(Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight), new Vector2(Resolution.ResolutionHandler.WindowWidth, Resolution.ResolutionHandler.WindowHeight));
            camera.Zoom = transaction.Value;
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

        public MenuStateEnum State
        {
            get
            {
                return currentState;
            }
        }

        Vector2 ButtonSize
        {
            get
            {
                return new Vector2(160, 100);
            }
        }

        float ButtonOffSet
        {
            get
            {
                return 5.0f;
            }
        }

        Vector2 Size
        {
            get
            {
                return new Vector2(ButtonSize.X + ButtonOffSet * 2, 5 * (ButtonSize.Y + ButtonOffSet));
            }

        }

        Vector2 Location
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y / 2);
            }
           
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
            DrawTextProperties textProperties = new DrawTextProperties("editor", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, ButtonOffSet),ButtonSize, this.MenuRectangle));

            textProperties.text = "new";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, ButtonSize.Y + ButtonOffSet), ButtonSize, this.MenuRectangle));

            textProperties.text = "load";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 2), ButtonSize, this.MenuRectangle));

            textProperties.text = "save";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 3), ButtonSize, this.MenuRectangle));

            textProperties.text = "settings";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(ButtonOffSet, (ButtonSize.Y + ButtonOffSet) * 4), ButtonSize, this.MenuRectangle));
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

            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            spriteBatch.End();

        }
    }
}
