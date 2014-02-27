using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;

    class LoadMapMenu : IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;

        #endregion

        #region Constructor

        public LoadMapMenu(ContentManager Content, MenuHandler menuHandler)
        {
            components = new List<AGUIComponent>();
            InitializeGUI(Content,menuHandler);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");

            Resolution.ResolutionHandler.Changed += ResetSizes;

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
            }
        }

        #endregion

        #region Initialize GUI

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("previous", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location - new Vector2(0, 0), ButtonSize, this.MenuRectangle));

            textProperties.text = "load";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X, 0), ButtonSize, this.MenuRectangle));

            textProperties.text = "next";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X*2, 0), ButtonSize, this.MenuRectangle));

            textProperties.text = "back";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(+ButtonSize.X, ButtonSize.Y), ButtonSize, this.MenuRectangle));
            components[3].StoreAndExecuteOnMouseRelease(new Actions.SwapWindowAction(menuHandler, "mainMenu"));

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
                                   
            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.Draw(backGround, UpperRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(backGround, LowerRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(backGround, LowerFrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

        }


    }
}
