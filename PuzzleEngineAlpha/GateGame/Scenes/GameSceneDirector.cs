using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GateGame.Scene
{
    using PuzzleEngineAlpha;
    using PuzzleEngineAlpha.Scene;

    public class GameSceneDirector : SceneDirector
    {

        #region Declarations

        Dictionary<string, IScene> gameScenes;

        #endregion

        #region Constructor

        public GameSceneDirector(GraphicsDevice graphicsDevice, ContentManager content,PuzzleEngineAlpha.Resolution.ResolutionHandler resolutionHandler)
            : base(graphicsDevice, content)
        {
            gameScenes = new Dictionary<string, IScene>();
            this.activeScenes = gameScenes;
            InitializeGameScenes(graphicsDevice, content,resolutionHandler);
            showGame = true;
        }

        #endregion

        #region Initialization

        void InitializeGameScenes(GraphicsDevice graphicsDevice, ContentManager content,PuzzleEngineAlpha.Resolution.ResolutionHandler resolutionHandler)
        {
            PuzzleEngineAlpha.Level.TileMap gameTileMap = new PuzzleEngineAlpha.Level.TileMap(Vector2.Zero, content, 64, 64, 64, 64);
            MapHandlerScene gameMapHandler = new MapHandlerScene(content, gameTileMap, new PuzzleEngineAlpha.Databases.Level.BinaryLevelInfoSerialization(), new PuzzleEngineAlpha.Databases.Level.BinaryMapSerialization());

            gameScenes.Add("game", new GameScene(graphicsDevice, content, gameTileMap, Vector2.Zero));
            gameScenes.Add("diagnostics", new DiagnosticsScene(graphicsDevice, content));
            gameScenes.Add("menu", new Menu.MenuHandler(content, graphicsDevice, gameMapHandler, gameTileMap,resolutionHandler,this));
            gameScenes.Add("mapHandler", gameMapHandler);

        }

        #endregion

        #region Helper Methods

        public override void ManageScenes()
        {
            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyReleased(PuzzleEngineAlpha.Input.ConfigurationManager.Config.ToggleDiagnostics))
            {
                if (showGame) return;
                if (activeScenes.ContainsKey("diagnostics"))
                {
                    PuzzleEngineAlpha.Level.Editor.TileManager.ShowPassable = false;
                    activeScenes.Remove("diagnostics");
                }
                else
                {
                    PuzzleEngineAlpha.Level.Editor.TileManager.ShowPassable = true;
                    activeScenes.Add("diagnostics", bgScenes["diagnostics"]);
                }
            }

            base.ManageScenes();
        }

        bool showGame;

        public override void SwapActiveScenes()
        {
            if (showGame)
            {
                activeScenes = editorScenes;
                showGame = false;
            }
            else
            {
                activeScenes = gameScenes;
                showGame = true;
            }
        }

        #endregion

    }
}