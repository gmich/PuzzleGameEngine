using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actions;
using PuzzleEngineAlpha.Components;
using PuzzleEngineAlpha.Components.Areas;
using PuzzleEngineAlpha.Components.Buttons;
using PuzzleEngineAlpha.Scene;

namespace GateGame.Scene.Menu
{
    
    class MainMenu : PuzzleEngineAlpha.Scene.IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;
        ComponentEnumerator enumerator;
        #endregion

        #region Constructor

        public MainMenu(ContentManager Content, MenuHandler menuHandler, GameSceneDirector sceneDirector)
        {
            enumerator = new ComponentEnumerator(Microsoft.Xna.Framework.Input.Keys.Down, Microsoft.Xna.Framework.Input.Keys.Up, Microsoft.Xna.Framework.Input.Keys.Enter);
            components = new List<AGUIComponent>();
            InitializeGUI(Content, menuHandler, sceneDirector);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");
            PuzzleEngineAlpha.Resolution.ResolutionHandler.Changed += ResetSizes;
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
                return new Vector2(ButtonSize.X , 5 * (ButtonSize.Y));
            }

        }

        Vector2 Location
        {
            get
            {
                return new Vector2(PuzzleEngineAlpha.Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, PuzzleEngineAlpha.Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y / 2);
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

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler, GameSceneDirector sceneDirector)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), PuzzleEngineAlpha.Scene.DisplayLayer.Menu, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), PuzzleEngineAlpha.Scene.DisplayLayer.Menu + 0.02f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), DisplayLayer.Menu + 0.01f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("play", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, PuzzleEngineAlpha.Scene.DisplayLayer.Menu + 0.03f, 1.0f);

            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, 0), ButtonSize, this.MenuRectangle));
            components[0].StoreAndExecuteOnMouseRelease(new ExitMenuAction());
            components[0].StoreAndExecuteOnMouseOver(new SetEnumeratorValueAction(this.enumerator,0));

            textProperties.text = "load";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, ButtonSize.Y), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.SwapGameWindowAction(menuHandler, "loadMap"));
            components[1].StoreAndExecuteOnMouseOver(new SetEnumeratorValueAction(this.enumerator, 1));

            textProperties.text = "settings";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (ButtonSize.Y) * 2), ButtonSize, this.MenuRectangle));
            components[2].StoreAndExecuteOnMouseRelease(new Actions.SwapGameWindowAction(menuHandler, "settings"));
            components[2].StoreAndExecuteOnMouseOver(new SetEnumeratorValueAction(this.enumerator, 2));

            textProperties.text = "editor";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (ButtonSize.Y) * 3), ButtonSize, this.MenuRectangle));
            components[3].StoreAndExecuteOnMouseRelease(new PuzzleEngineAlpha.Actions.ToggleActiveSceneryAction((PuzzleEngineAlpha.Scene.SceneDirector)sceneDirector));
            components[3].StoreAndExecuteOnMouseOver(new SetEnumeratorValueAction(this.enumerator, 3));

            textProperties.text = "exit";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (ButtonSize.Y) * 4), ButtonSize, this.MenuRectangle));
            components[4].StoreAndExecuteOnMouseRelease(new Actions.TerminateGameAction());
            components[4].StoreAndExecuteOnMouseOver(new SetEnumeratorValueAction(this.enumerator, 4));

            foreach (AGUIComponent component in components)
                enumerator.AddGUIComponent(component);

            components[0].IsFocused = true;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
            enumerator.HandleSelection();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu + 0.02f);
            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu + 0.01f);  

            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }
        }

    }
}
