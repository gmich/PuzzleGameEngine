using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha;
using PuzzleEngineAlpha.Components;
using PuzzleEngineAlpha.Components.ScrollBars;
using PuzzleEngineAlpha.Scene;
using PuzzleEngineAlpha.Actions;
using PuzzleEngineAlpha.Components.Areas;

namespace GateGame.Scene.Menu
{
    class LoadMapMenu : PuzzleEngineAlpha.Scene.IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;
        PuzzleEngineAlpha.Scene.MapHandlerScene mapHandler;
        PuzzleEngineAlpha.Level.MiniMap miniMap;
        PuzzleEngineAlpha.Level.TileMap tileMap;
        ComponentEnumerator enumerator;

        #endregion

        #region Constructor

        public LoadMapMenu(GraphicsDevice graphicsDevice, ContentManager Content, MenuHandler menuHandler, PuzzleEngineAlpha.Scene.MapHandlerScene mapHandler, PuzzleEngineAlpha.Level.TileMap tileMap)
        {
            this.tileMap = tileMap;
            enumerator = new ComponentEnumerator(Microsoft.Xna.Framework.Input.Keys.Right, Microsoft.Xna.Framework.Input.Keys.Left, Microsoft.Xna.Framework.Input.Keys.Enter); 
            miniMap = new PuzzleEngineAlpha.Level.MiniMap(Content, graphicsDevice, new Vector2(400, 300), new PuzzleEngineAlpha.Databases.Level.BinaryMapSerialization(), new PuzzleEngineAlpha.Databases.Level.BinaryLevelInfoSerialization());
            components = new List<AGUIComponent>();
            InitializeGUI(Content,menuHandler);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");
            this.mapHandler = mapHandler;
            PuzzleEngineAlpha.Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            components[0].Position = Location + new Vector2(0, 0);
            components[0].GeneralArea = this.MenuRectangle;
            components[1].Position = Location + new Vector2(+ButtonSize.X, 0);
            components[1].GeneralArea = this.MenuRectangle;
            components[2].Position = Location + new Vector2(+ButtonSize.X * 2, 0);
            components[2].GeneralArea = this.MenuRectangle;
            components[3].Position = Location + new Vector2(+ButtonSize.X, ButtonSize.Y);
            components[3].GeneralArea = this.MenuRectangle;
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
                return new Vector2(ButtonSize.X*3 , 2 * ButtonSize.Y);
            }

        }

        Vector2 Location
        {
            get
            {
                return new Vector2(PuzzleEngineAlpha.Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, PuzzleEngineAlpha.Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y / 2 + 130);
            }
           
        }

        Rectangle MenuRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        Rectangle UpperRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y/2);
            }
        }

        Rectangle LowerRectangle
        {
            get
            {
                return new Rectangle((int)Location.X + (int)ButtonSize.X, (int)Location.Y + (int)ButtonSize.Y, (int)Size.X - (int)ButtonSize.X*2, (int)Size.Y - (int)ButtonSize.Y);
            }
        }

        Rectangle LowerFrameRectangle
        {
            get
            {
                int offSet = 1;
                return new Rectangle((int)Location.X + (int)ButtonSize.X - offSet, (int)Location.Y + (int)ButtonSize.Y - offSet, (int)Size.X - (int)ButtonSize.X * 2 + offSet * 2, (int)Size.Y - (int)ButtonSize.Y + offSet * 2);
            }
        }

        Rectangle FrameRectangle
        {
            get
            {
                int offSet = 1;
                return new Rectangle((int)Location.X - offSet, (int)Location.Y - offSet, (int)Size.X + offSet * 2, (int)Size.Y/2 + offSet * 2);
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

                if(value==true)
                    miniMap.Refresh();
            }
        }

        #endregion

        #region Initialize GUI

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), DisplayLayer.Menu, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), DisplayLayer.Menu+0.02f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), DisplayLayer.Menu + 0.01f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("previous", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, DisplayLayer.Menu + 0.03f, 1.0f);
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, 0), ButtonSize, this.MenuRectangle));
            components[0].StoreAndExecuteOnMouseRelease((new PuzzleEngineAlpha.Actions.ChangeMiniMapAction(this.miniMap, -1)));

            textProperties.text = "load";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X, 0), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new PuzzleEngineAlpha.Actions.LoadMapAction(this.miniMap, this.tileMap));

            textProperties.text = "next";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X * 2, 0), ButtonSize, this.MenuRectangle));
            components[2].StoreAndExecuteOnMouseRelease((new PuzzleEngineAlpha.Actions.ChangeMiniMapAction(this.miniMap, +1)));

            textProperties.text = "back";
            components.Add(new PuzzleEngineAlpha.Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X, ButtonSize.Y), ButtonSize, this.MenuRectangle));
            components[3].StoreAndExecuteOnMouseRelease(new Actions.SwapGameWindowAction(menuHandler, "mainMenu"));

            foreach (AGUIComponent component in components)
                enumerator.AddGUIComponent(component);

            components[0].IsFocused = true;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            miniMap.Update(gameTime);

            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
            enumerator.HandleSelection();

        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backGround, UpperRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.01f);
            spriteBatch.Draw(backGround, LowerRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.01f);
            spriteBatch.Draw(backGround, LowerFrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.02f);
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, DisplayLayer.Menu - 0.02f);

            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }

            miniMap.Draw(spriteBatch);

        }

        #endregion

    }
}
