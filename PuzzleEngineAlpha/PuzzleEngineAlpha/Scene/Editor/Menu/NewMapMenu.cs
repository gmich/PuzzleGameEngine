using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    using Components;
    using Components.TextBoxes;

    class NewMapMenu : IScene
    {

        #region Declarations

        List<AGUIComponent> components;
        Texture2D backGround;
        Dictionary<string, TextBox> textboxes;
        List<Animations.DisplayMessage> messages;
 
        #endregion

        #region Constructor

        public NewMapMenu(ContentManager Content,MenuHandler menuHandler,Level.TileMap tileMap)
        {
            textboxes = new Dictionary<string, TextBox>();
            messages = new List<Animations.DisplayMessage>();

            components = new List<AGUIComponent>();

            InitializeGUI(Content, menuHandler, tileMap);
            backGround = Content.Load<Texture2D>(@"textures/whiteRectangle");
            Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Position = this.Location + new Vector2(0, ButtonSize.Y * i +(TextBoxHeight * 4));
                components[i].GeneralArea = this.MenuRectangle;
            }

            int x=0;
            foreach(TextBox textbox in textboxes.Values)
            {
                textbox.Location= Location + new Vector2(0, TextBoxHeight*x);
                x++;
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

        int TextBoxWidth
        {
            get
            {
                return 45;
            }
        }

        int TextBoxHeight
        {
            get
            {
                return 32;
            }
        }

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
                return new Vector2(ButtonSize.X, 2 * ButtonSize.Y + (TextBoxHeight*4));
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

        void InitializeGUI(ContentManager Content, MenuHandler menuHandler,Level.TileMap tileMap)
        {
            int messageOffset = 148;
            textboxes.Add("initialID", new TextBox(new Input.KeyboardNumberInput(), Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), Location, TextBoxWidth, TextBoxHeight));
            messages.Add(new Animations.DisplayMessage(Content, new Vector2(-23, +messageOffset), "initial id", -1));
            textboxes["initialID"].Text = "0";

            textboxes.Add("tilesize", new TextBox(new Input.KeyboardNumberInput(), Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), Location + new Vector2(0, TextBoxHeight), TextBoxWidth, TextBoxHeight));
            messages.Add(new Animations.DisplayMessage(Content, new Vector2(-23, +messageOffset - TextBoxHeight), "tile size ", -1));
            textboxes["tilesize"].Text = "64";
            textboxes.Add("mapwidth", new TextBox(new Input.KeyboardNumberInput(), Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), Location + new Vector2(0, 2*TextBoxHeight), TextBoxWidth, TextBoxHeight));
            messages.Add(new Animations.DisplayMessage(Content, new Vector2(-23, +messageOffset - TextBoxHeight * 2), "map width ", -1));
            textboxes["mapwidth"].Text = "100";
            textboxes.Add("mapheight", new TextBox(new Input.KeyboardNumberInput(), Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), Location + new Vector2(0, 3*TextBoxHeight), TextBoxWidth, TextBoxHeight));
            textboxes["mapheight"].Text = "100";
            messages.Add(new Animations.DisplayMessage(Content, new Vector2(-23, +messageOffset - TextBoxHeight * 3), "map height", -1));

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("new map", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, (TextBoxHeight* 4)), ButtonSize, this.MenuRectangle));
            components[0].StoreAndExecuteOnMouseRelease(new Actions.NewMapAction(textboxes["initialID"], textboxes["tilesize"], textboxes["mapwidth"], textboxes["mapheight"], tileMap));
            textProperties.text = "back";
            components.Add(new Components.Buttons.MenuButton(button, frame, clickedButton, textProperties, Location + new Vector2(0, ButtonSize.Y + (TextBoxHeight * 4)), ButtonSize, this.MenuRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.SwapWindowAction(menuHandler, "mainMenu"));

        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
            {
                component.Update(gameTime);
            }
            foreach (TextBox textbox in textboxes.Values)
            {
                textbox.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Animations.DisplayMessage message in messages)
            {
                message.Draw(spriteBatch);
            }                   
            foreach (AGUIComponent component in components)
            {
                component.Draw(spriteBatch);
            }
            foreach (TextBox textbox in textboxes.Values)
            {
                textbox.Draw(spriteBatch);
            }


            spriteBatch.Draw(backGround, MenuRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(backGround, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);


        }

    }
}
