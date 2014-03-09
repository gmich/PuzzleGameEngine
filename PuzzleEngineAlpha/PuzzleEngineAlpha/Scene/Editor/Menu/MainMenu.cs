using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;

    class MainMenu : IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;

        #endregion

        #region Constructor

        public MainMenu(ContentManager Content,MenuHandler menuHandler,SceneDirector sceneDirector)
        {
            components = new List<AGUIComponent>();
            InitializeGUI(Content, menuHandler, sceneDirector);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");

            Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(0, ButtonSize.Y * i);
                components[i].GeneralArea = this.MenuRectangle;
            }
        }

        #endregion    

        #region Helper Methods

        public void GoInactive()
        {
            isActive = false;
        }

        #endregion

        #region Properties

        Vector2 ButtonSize
        {
            get
            {
                return new Vector2(160, 100);
            }
        }

        Vector2 Size
        {
            get
            {
                return new Vector2(ButtonSize.X , 6 * (ButtonSize.Y));
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

        Rectangle FrameRectangle
        {
            get
            {
                int offSet = 1;
                return new Rectangle((int)Location.X - offSet, (int)Location.Y - offSet, (int)Size.X + offSet * 2, (int)Size.Y + offSet*2);
            }
        }

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

        #region Initialize GUI

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler,SceneDirector sceneDirector)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), Scene.DisplayLayer.Menu, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), Scene.DisplayLayer.Menu+0.02f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), Scene.DisplayLayer.Menu + 0.01f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("editor", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, Scene.DisplayLayer.Menu + 0.03f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0,0),ButtonSize, this.MenuRectangle));
            components[0].StoreAndExecuteOnMouseRelease(new Actions.ExitMenuAction());

            textProperties.text = "new";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, ButtonSize.Y), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.SwapEditorWindowAction(menuHandler, "newMap"));

            textProperties.text = "load";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (ButtonSize.Y) * 2), ButtonSize, this.MenuRectangle));
            components[2].StoreAndExecuteOnMouseRelease(new Actions.SwapEditorWindowAction(menuHandler, "loadMap"));

            textProperties.text = "save";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(0, (ButtonSize.Y ) * 3), ButtonSize, this.MenuRectangle));
            components[3].StoreAndExecuteOnMouseRelease(new Actions.SwapEditorWindowAction(menuHandler, "saveMap"));

            textProperties.text = "game";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location +  new Vector2(0, (ButtonSize.Y) * 4), ButtonSize, this.MenuRectangle));
            components[4].StoreAndExecuteOnMouseRelease(new Actions.ToggleActiveSceneryAction(sceneDirector));

            textProperties.text = "exit";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (ButtonSize.Y) * 5), ButtonSize, this.MenuRectangle));
            components[5].StoreAndExecuteOnMouseRelease(new Actions.TerminateGameAction());
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Menu + 0.02f);
            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Menu + 0.01f);  

            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }
        }

    }
}
