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

            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 20), new Vector2(90, 90), Resolution.ResolutionHandler.ScreenRectangle));

            textProperties.text = "button 2";
            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 120), new Vector2(90, 90), Resolution.ResolutionHandler.ScreenRectangle));

            textProperties.text = "button 3";
            guiComponents.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(40, 220), new Vector2(90, 90), Resolution.ResolutionHandler.ScreenRectangle));
            Initialize();
        }

        public void Initialize()
        {

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
