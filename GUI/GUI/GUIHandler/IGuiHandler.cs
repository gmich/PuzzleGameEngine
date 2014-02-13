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
            DrawTextProperties textProperties = new DrawTextProperties("test", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);
       
            guiElements.Add(new MenuButton(button,frame,clickedButton,textProperties,new Vector2(20,20), new Vector2(200,50)));
            Initialize();
        }

        public void Initialize()
        {
            Player player = new Player();
            guiElements[0].StoreAndExecuteOnMouseClick(new InvokePlayerAction(player));
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
