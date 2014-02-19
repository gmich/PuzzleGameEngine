using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.GUIManager
{
    using Actions;
    using Components;
    using ActionReceivers;
    using Components.Buttons;

    public class GUIHandler
    {
        List<AGUIComponent> guiComponents;

        public GUIHandler(ContentManager Content)
        {
            guiComponents = new List<AGUIComponent>();

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("button 1", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 20), new Vector2(90, 90)));

            textProperties.text = "button 2";
            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 120), new Vector2(90, 90)));

            textProperties.text = "button 3";
            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 220), new Vector2(90, 90)));
            Initialize();
        }

        public void Initialize()
        {
            Player player = new Player();
            guiComponents[0].StoreAndExecuteOnMouseClick(new AppendConsoleText("button1 - clicked"));
            guiComponents[1].StoreAndExecuteOnMouseClick(new AppendConsoleText("button2 - clicked"));
            guiComponents[2].StoreAndExecuteOnMouseClick(new AppendConsoleText("button2 - clicked"));

            guiComponents[0].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button1 - released"));
            guiComponents[1].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button2 - released"));
            guiComponents[2].StoreAndExecuteOnMouseRelease(new AppendConsoleText("button3 - released"));

            guiComponents[0].StoreAndExecuteOnMouseOver(new AppendConsoleText("button1 - mouserOver"));
            guiComponents[1].StoreAndExecuteOnMouseOver(new AppendConsoleText("button2 - mouserOver"));
            guiComponents[2].StoreAndExecuteOnMouseOver(new AppendConsoleText("button3 - mouserOver"));

            guiComponents[0].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button1 - mouserLeave"));
            guiComponents[1].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button2 - mouserLeave"));
            guiComponents[2].StoreAndExecuteOnMouseLeave(new AppendConsoleText("button3 - mouserLeave"));
        }

        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in guiComponents)
                component.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AGUIComponent component in guiComponents)
                component.Draw(spriteBatch);
        }
    }
}
