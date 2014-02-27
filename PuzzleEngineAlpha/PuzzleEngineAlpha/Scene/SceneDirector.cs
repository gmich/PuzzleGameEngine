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

            ToggleMenuTrigger = false;
            bgScenes.Add("diagnostics", new Editor.DiagnosticsScene(graphicsDevice, content));
            scenes.Add("config", new Editor.ConfigurationScene(graphicsDevice, content, new Vector2(170, 210)));
            scenes.Add("selection", new Editor.SelectionScene(graphicsDevice, content, 64, 64, new Vector2(170, Resolution.ResolutionHandler.WindowHeight - 215)));
            scenes.Add("map", new Editor.MapScene(graphicsDevice, content, 64, 64, Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth - 170, Resolution.ResolutionHandler.WindowHeight)));
            scenes.Add("editorMenu", new Editor.Menu.MenuHandler(content, graphicsDevice));    

            BringToFront("diagnostics");

        }

        #endregion

        #region Helper Methods

        public void BringToFront(string scene)
        {
            if (!scenes.ContainsKey(scene) && bgScenes.ContainsKey(scene))
                scenes.Add(scene, bgScenes[scene]);
                
        }

        public static bool ToggleMenuTrigger
        {
            get;
            set;
        }

        public void ManageScenes()
        {

            if (Input.InputHandler.IsKeyReleased(Input.ConfigurationManager.Config.ToggleDiagnostics))
            {
                if (scenes.ContainsKey("diagnostics"))
                {
                    Level.Editor.TileManager.ShowPassable = false;
                    scenes.Remove("diagnostics");
                }
                else
                {
                    Level.Editor.TileManager.ShowPassable = true;
                    scenes.Add("diagnostics", bgScenes["diagnostics"]);
                }
            }

            if (Input.InputHandler.IsKeyReleased(Input.ConfigurationManager.Config.ToggleMenu) || ToggleMenuTrigger)
            {
                ToggleMenuTrigger = false;

                if (scenes.ContainsKey("editorMenu"))
                {
                    if (scenes["editorMenu"].IsActive)
                    {
                        foreach (IScene scene in scenes.Values)
                        {
                            if(scene!=scenes["editorMenu"])
                            scene.IsActive = true;
                        }
                        scenes["editorMenu"].GoInactive();
                    }
                    else
                    {
                        foreach (IScene scene in scenes.Values)
                        {
                            if (scene != scenes["editorMenu"])
                                scene.IsActive = false;
                        }
                        scenes["editorMenu"].IsActive = true;
                    }
                }
            }
        }

        #endregion

        public static bool ShowMenu
        {
            get;
            set;
        }

        public void Update(GameTime gameTime)
        {
            ManageScenes();

            foreach (IScene scene in scenes.Values)
            {
                if(scene.IsActive)
                    scene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IScene scene in scenes.Values)
                scene.Draw(spriteBatch);
        }
    }
}
