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

        protected Dictionary<string,IScene> editorScenes;
        protected Dictionary<string, IScene> activeScenes;
        protected Dictionary<string, IScene> bgScenes;

        #endregion

        #region Constructor

        public SceneDirector(GraphicsDevice graphicsDevice,ContentManager content)
        {
            editorScenes = new Dictionary<string, IScene>();
            //gameScenes  = new Dictionary<string, IScene>();
            activeScenes = new Dictionary<string, IScene>();
            bgScenes = new Dictionary<string, IScene>();

            bgScenes.Add("diagnostics", new Editor.DiagnosticsScene(graphicsDevice, content));

            InitializeEditorScenes(graphicsDevice,content);

            ToggleMenuTrigger = true;
        }

        #endregion

        #region Initialization

        void InitializeEditorScenes(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Level.Editor.EditorTileMap tileMap = new Level.Editor.EditorTileMap(Vector2.Zero, content, 64, 64, 64, 64, true);
            MapHandlerScene mapHandler = new MapHandlerScene(content, tileMap, new Databases.Level.BinaryLevelInfoSerialization(), new Databases.Level.BinaryMapSerialization());
            this.activeScenes = editorScenes;

            editorScenes.Add("selectionActors", new Editor.SelectionSceneActors(graphicsDevice, content, 64, 64, new Vector2(170, Resolution.ResolutionHandler.WindowHeight - 215)));
            editorScenes.Add("selection", new Editor.SelectionScene(graphicsDevice, content, 64, 64, new Vector2(170, Resolution.ResolutionHandler.WindowHeight - 215)));
            editorScenes.Add("config", new Editor.ConfigurationScene(graphicsDevice, content, new Vector2(170, 210), editorScenes["selectionActors"], editorScenes["selection"]));
            editorScenes.Add("map", new Editor.MapScene(tileMap, graphicsDevice, content, 64, 64, Vector2.Zero, new Vector2(Resolution.ResolutionHandler.WindowWidth - 170, Resolution.ResolutionHandler.WindowHeight)));

            BringToFront("diagnostics");
            editorScenes.Add("menu", new Editor.Menu.MenuHandler(content, graphicsDevice, mapHandler, tileMap,this));
            editorScenes.Add("mapHandler", mapHandler);

        }


        #endregion
        #region Helper Methods

        public virtual void SwapActiveScenes()
        { }

        public void BringToFront(string scene)
        {
            if (!activeScenes.ContainsKey(scene) && bgScenes.ContainsKey(scene))
                activeScenes.Add(scene, bgScenes[scene]);
        }

        public static bool ToggleMenuTrigger
        {
            get;
            set;
        }


        public virtual void ManageScenes()
        {
            

            if (Input.InputHandler.IsKeyReleased(Input.ConfigurationManager.Config.ToggleMenu) || ToggleMenuTrigger)
            {
                ToggleMenuTrigger = false;

                if (activeScenes.ContainsKey("menu"))
                {
                    if (activeScenes["menu"].IsActive)
                    {
                        foreach (IScene scene in activeScenes.Values)
                        {
                            if (scene != activeScenes["menu"])
                            scene.IsActive = true;
                        }
                        activeScenes["menu"].GoInactive();
                    }
                    else
                    {
                        foreach (IScene scene in activeScenes.Values)
                        {
                            if (scene != activeScenes["menu"])
                                scene.IsActive = false;
                        }
                        activeScenes["menu"].IsActive = true;
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

            foreach (IScene scene in activeScenes.Values)
            {
                if(scene.IsActive)
                    scene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IScene scene in activeScenes.Values)   
                scene.Draw(spriteBatch);

        }
    }
}
