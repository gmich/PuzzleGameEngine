using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleEngineAlpha.Level.Editor
{
    using Camera;
    using Input;
    using Components;

    public class EditorTileMap : TileMap
    {
        #region Declarations

        Texture2D frameTexture;
        EditorMapSquare[,] editorMapSquares;
        ContentManager Content;
        Rectangle sceneRectangle;

        #endregion

        #region Constructor

        public EditorTileMap(Vector2 cameraPosition, ContentManager Content, int sourceTileWidth, int sourceTileHeight, int tileWidth, int tileHeight, bool showGrid)
            : base(cameraPosition, Content, sourceTileWidth, sourceTileHeight, tileWidth, tileHeight)
        {
            this.frameTexture = Content.Load<Texture2D>(@"Buttons/tileFrame");
            this.ShowGrid = showGrid;
            this.Content = Content;
        }

        #endregion

        #region Initialize Map

        public void HandleResolutionChange(Rectangle newSceneRectangle)
        {
            this.sceneRectangle = newSceneRectangle;
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    editorMapSquares[x, y].GeneralArea = newSceneRectangle;
                }
            } 
        }

        public void InitializeButtons(Rectangle sceneRectangle)
        {
            this.sceneRectangle = sceneRectangle;

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Textures/PlatformTilesTemp"), Scene.DisplayLayer.Tile, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/tileFrame"), Scene.DisplayLayer.TileFrame, 1.0f, 0.0f, Color.White);
            DrawTextProperties passableText = new DrawTextProperties("x", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, Scene.DisplayLayer.TileFrame+0.01f, 1.0f);
            DrawTextProperties codeValueText = new DrawTextProperties("", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, Scene.DisplayLayer.TileFrame + 0.01f, 1.0f);
            Texture2D background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            editorMapSquares = new EditorMapSquare[MapWidth, MapHeight];
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    editorMapSquares[x, y] = new EditorMapSquare(passableText,codeValueText,background,button, frame, new Vector2(x * TileWidth, y * TileHeight), new Vector2(TileWidth, TileHeight), TileSourceRectangle(mapCells[x, y].LayerTile), this.Camera, sceneRectangle, mapCells[x, y].LayerTile);
                    editorMapSquares[x, y].MapSquare = mapCells[x, y];
                    editorMapSquares[x, y].StoreAndExecuteOnMouseDown(new Actions.SetEditorMapSquare(editorMapSquares[x, y]));
                    editorMapSquares[x, y].StoreAndExecuteOnMouseRelease(new Actions.SetEditorSelectedTileAction(editorMapSquares[x, y]));
                }
            }
        }

        public override void SetMapCells(MapSquare[,] mapSquares)
        {
            this.mapCells = mapSquares;
            this.InitializeButtons(sceneRectangle);
        }

        #endregion

        #region Properties

        public bool ShowGrid
        {
            get;
            set;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            for (int x = StartX; x <= EndX; x++)
                for (int y = StartY; y <= EndY; y++)
                {
                    editorMapSquares[x, y].Update(gameTime);
                }
        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 30), "X: " + StartX + " / " + MapWidth + "  Y: " + StartY + " / " + MapHeight);
            Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 55), "scale: " + Camera.Zoom);

            for (int x = StartX; x <= EndX; x++)
                for (int y = StartY; y <= EndY; y++)
                {
                    {
                        if (ShowGrid)
                            spriteBatch.Draw(frameTexture, CellScreenRectangle(x, y), null,
                                   Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.Grid);
                        editorMapSquares[x, y].Draw(spriteBatch);

                    }
                }

            // base.Draw(spriteBatch);
        }

        #endregion

    }
}
