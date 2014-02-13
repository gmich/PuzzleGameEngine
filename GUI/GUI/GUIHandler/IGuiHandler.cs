using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GUI.GUIManager
{
    using Actions;
    using Components;
    using ActionReceivers;
    using Components.Buttons;

    public class GUIHandler
    {
        List<AGUIElement> guiElements;

        public GUIHandler(ContentManager Content)
        {
            guiElements = new List<AGUIElement>();

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("button 1", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            guiElements.Add(new MenuButton(button,frame,clickedButton,textProperties,new Vector2(40,20), new Vector2(90,90)));

            textProperties.text = "button 2";
            guiElements.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 120), new Vector2(90, 90)));

            textProperties.text = "button 3";
            guiElements.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 220), new Vector2(90, 90)));
            Initialize();
        }

        public void Initialize()
        {
            Player player = new Player();
            guiElements[0].StoreAndExecuteOnMouseClick(new AppendConsoleText("button1 - clicked"));
            guiElements[1].StoreAndExecuteOnMouseClick(new AppendConsoleText("button2 - clicked"));
            guiElements[2].StoreAndExecuteOnMouseClick(new AppendConsoleText("button2 - clicked"));

            guiElements[0].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button1 - released"));
            guiElements[1].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button2 - released"));
            guiElements[2].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button3 - released"));

            guiElements[0].StoreAndExecuteOnMouseOver(new AppendConsoleText("button1 - mouserOver"));
            guiElements[1].StoreAndExecuteOnMouseOver(new AppendConsoleText("button2 - mouserOver"));
            guiElements[2].StoreAndExecuteOnMouseOver(new AppendConsoleText("button3 - mouserOver"));

            guiElements[0].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button1 - mouserLeave"));
            guiElements[1].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button2 - mouserLeave"));
            guiElements[2].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button3 - mouserLeave"));
        }

        public void Update(GameTime gameTime)
        {
            foreach (AGUIElement element in guiElements)
                element.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AGUIElement element in guiElements)
                element.Draw(spriteBatch);
        }
    }
}
