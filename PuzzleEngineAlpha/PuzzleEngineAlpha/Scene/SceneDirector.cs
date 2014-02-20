using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene
{
    public class SceneDirector
    {

        #region Declarations

        Dictionary<string,IScene> scenes;
        Dictionary<string, IScene> bgScenes;

        #endregion

        #region Constructor

        public SceneDirector(GraphicsDevice graphicsDevice,ContentManager content)
        {
            scenes = new Dictionary<string,IScene>();
            bgScenes = new Dictionary<string, IScene>();

            scenes.Add("gamescene",new GameScene(graphicsDevice, content));
            bgScenes.Add("diagnostics", new DiagnosticsScene(graphicsDevice, content));
        }

        #endregion

        #region Helper Methods

        public void UpdateRenderTargets()
        {
            foreach (IScene scene in scenes.Values)
                scene.UpdateRenderTarget();
        }

        public void ManageScenes()
        {

            if (Input.InputHandler.IsKeyReleased(Input.ConfigurationManager.Config.ToggleDiagnostics))
            {
                if (scenes.ContainsKey("diagnostics")) scenes.Remove("diagnostics");
                else
                    scenes.Add("diagnostics", bgScenes["diagnostics"]);
            }
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            ManageScenes();

            foreach (IScene scene in scenes.Values)
                scene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IScene scene in scenes.Values)
                scene.Draw(spriteBatch);
        }
    }
}
