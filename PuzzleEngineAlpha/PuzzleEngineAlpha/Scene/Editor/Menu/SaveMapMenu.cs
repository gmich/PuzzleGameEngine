using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;
    using Components.TextBoxes;

    class SaveMapMenu : IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;
        TextBox textBox;
        #endregion

        #region Constructor

        public SaveMapMenu(ContentManager Content, MenuHandler menuHandler)
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
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(0, ButtonSize.Y * i + 30);
                components[i].GeneralArea = this.MenuRectangle;
            }
            textBox.Location = Location;
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
                return new Vector2(ButtonSize.X , 2 * ButtonSize.Y + 30);
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

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler)
        {
            textBox = new TextBox(Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), Location, (int)ButtonSize.X, 30);
            textBox.Text = "mapName";
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("save", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, 30), ButtonSize, this.MenuRectangle));

            textProperties.text = "back";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, ButtonSize.Y+30 ), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.SwapWindowAction(menuHandler, "mainMenu"));


        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
            textBox.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            textBox.Draw(spriteBatch);
       
            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        }


    }
}
