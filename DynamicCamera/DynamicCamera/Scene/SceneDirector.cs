using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DynamicCamera.Scene
{
    public class SceneDirector
    {
        List<IScene> scenes;

        public SceneDirector(GraphicsDevice graphicsDevice,ContentManager content)
        {
            scenes = new List<IScene>();
            scenes.Add(new GameScene(graphicsDevice,content));
        }

        public void UpdateRenderTargets()
        {
            foreach (IScene scene in scenes)
                scene.UpdateRenderTarget();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IScene scene in scenes)
                scene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IScene scene in scenes)
                scene.Draw(spriteBatch);
        }
    }
}
