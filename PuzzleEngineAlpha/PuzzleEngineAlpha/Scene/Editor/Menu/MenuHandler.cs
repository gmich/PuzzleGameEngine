using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Scene.Editor.Menu
{
    class MenuHandler : IScene
    {
        #region Declarations

        List<IMenuWindow> menuWindows;

        #endregion

        public MenuHandler(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            menuWindows = new List<IMenuWindow>();
            menuWindows.Add(new MainMenu(Content, graphicsDevice, new Vector2(200, 400), new Vector2(100, 100)));
            menuWindows[0].Show();
        }
        
        public void Update(GameTime gameTime)
        {
            if(Input.InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Z))
            menuWindows[0].Show();

            if(Input.InputHandler.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.X))
            menuWindows[0].Hide();

            foreach (IMenuWindow menuWindow in menuWindows)
                menuWindow.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IMenuWindow menuWindow in menuWindows)
                menuWindow.Draw(spriteBatch);
        }
    }
}