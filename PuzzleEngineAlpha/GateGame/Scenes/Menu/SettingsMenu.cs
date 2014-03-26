using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Scene;
using PuzzleEngineAlpha.Components.Buttons;
using PuzzleEngineAlpha.Components;
using PuzzleEngineAlpha.Components.Areas;
using PuzzleEngineAlpha.Camera;
using PuzzleEngineAlpha.Components.ScrollBars;

namespace GateGame.Scene.Menu
{
    
    class SettingsMenu : IScene
    {

        #region Declarations

        List<PuzzleEngineAlpha.Components.AGUIComponent> components;
        Texture2D backGround;
      //  ComponentEnumerator enumerator;
        Camera camera;
        VScrollBar vScrollBar;

        #endregion

        #region Constructor

        public SettingsMenu(ContentManager Content, MenuHandler menuHandler,PuzzleEngineAlpha.Resolution.ResolutionHandler resolutionHandler)
        {
            components = new List<AGUIComponent>();
           // enumerator = new ComponentEnumerator(Microsoft.Xna.Framework.Input.Keys.Down, Microsoft.Xna.Framework.Input.Keys.Up, Microsoft.Xna.Framework.Input.Keys.Enter);
            InitializeGUI(Content, menuHandler, resolutionHandler);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");
            PuzzleEngineAlpha.Resolution.ResolutionHandler.Changed += ResetSizes;

        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            vScrollBar.Size = new Vector2(ScrollBarWidth, Size.Y - 5);

            this.camera.ViewPortHeight = (int)GUIAreaSize.Y;

            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(0, ButtonSize.Y * i);
                components[i].GeneralArea = this.MenuRectangle;
            }
            vScrollBar.BarLocation = new Vector2(this.Size.X - ScrollBarWidth, 0) + Location;
            vScrollBar.bulletLocation = new Vector2(this.Size.X - ScrollBarWidth + Location.X, vScrollBar.BulletLocation.Y);

        }

        #endregion    

        #region Helper Methods

        public void GoInactive()
        {
            isActive = false;
        }

        #endregion

        #region Properties

        float ScrollBarWidth
        {
            get
            {
                return 20.0f;
            }
        }

        Vector2 ButtonSize
        {
            get
            {
                return new Vector2(160, 100);
            }
        }

        Vector2 GUIAreaSize
        {
            get
            {
                return new Vector2(this.ButtonSize.X, PuzzleEngineAlpha.Resolution.ResolutionHandler.WindowHeight);
            }

        }

        Vector2 Size
        {
            get
            {
                return new Vector2(ButtonSize.X, ButtonCount * ButtonSize.Y);
            }

        }

        int ButtonCount
        {
            get;
            set;
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
                return new Rectangle((int)Location.X - offSet, (int)Location.Y - offSet, (int)Size.X + offSet * 2, (int)Size.Y + offSet * 2);
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

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler, PuzzleEngineAlpha.Resolution.ResolutionHandler resolutionHandler)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), DisplayLayer.Menu, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), DisplayLayer.Menu + 0.02f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), DisplayLayer.Menu + 0.01f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("back", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, DisplayLayer.Menu + 0.03f, 1.0f);

            components.Add(new MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, 0), ButtonSize, this.MenuRectangle));
            components[0].StoreAndExecuteOnMouseRelease(new Actions.SwapGameWindowAction(menuHandler, "mainMenu"));
           // components[0].StoreAndExecuteOnMouseOver(new PuzzleEngineAlpha.Actions.SetEnumeratorValueAction(this.enumerator, 0));
            ButtonCount = 1;
            components[0].Camera = this.camera;

            int i = 1;
            foreach (var resolution in PuzzleEngineAlpha.Resolution.ResolutionHandler.GetSupportedResolutions())
            {
                textProperties.text = resolution.Key;
                components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, i * ButtonSize.Y), ButtonSize, this.MenuRectangle));
                components[i].StoreAndExecuteOnMouseRelease(new PuzzleEngineAlpha.Actions.ApplyResolutionAction(resolutionHandler, resolution.Value));
             //   components[i].StoreAndExecuteOnMouseOver(new PuzzleEngineAlpha.Actions.SetEnumeratorValueAction(this.enumerator, i));
                components[i].Camera = camera;
                i++;
                ButtonCount++;
            }
         //   foreach (AGUIComponent component in components)
             //   enumerator.AddGUIComponent(component);

            components[0].IsFocused = true;

            camera = new Camera(Vector2.Zero, GUIAreaSize, Size);

            vScrollBar = new VScrollBar(Content.Load<Texture2D>(@"ScrollBars/bullet"), Content.Load<Texture2D>(@"ScrollBars/bar"), camera, new Vector2(this.Size.X - ScrollBarWidth, 0) + Location, new Vector2(ScrollBarWidth, Size.Y - 5), PuzzleEngineAlpha.Scene.DisplayLayer.Editor + 0.1f);

            ResetSizes(this, EventArgs.Empty);
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
           // enumerator.HandleSelection();

            vScrollBar.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.01f);
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.02f);
               
            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }

            vScrollBar.Draw(spriteBatch);

        }


    }
}
