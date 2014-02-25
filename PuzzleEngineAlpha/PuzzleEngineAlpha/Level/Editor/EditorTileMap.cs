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
     
        #endregion

        #region Constructor

        public EditorTileMap(Vector2 cameraPosition, ContentManager Content, int tileWidth, int tileHeight,bool showGrid)
            : base(cameraPosition, Content, tileWidth, tileHeight)
        {
            this.frameTexture = Content.Load<Texture2D>(@"Buttons/tileFrame");
            this.ShowGrid = showGrid;      
        }

        #endregion

        #region Save

        public void SaveMap(FileStream fileStream)
        {
            //pass this as a readonly value in the constructor
            Databases.Level.IMapDB mapDB = new Databases.Level.BinaryMapSerialization();
        }       

        #endregion

        #region Initialize Map

        public override void Randomize(int mapWidth, int mapHeight)
        {
            base.Randomize(mapWidth, mapHeight);
            editorMapSquares = new EditorMapSquare[mapWidth, mapHeight];
            
        }

        public void HandleResolutionChange(Rectangle newSceneRectangle)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    editorMapSquares[x, y].GeneralArea = newSceneRectangle;
                }
            } 
        }

        public void InitializeButtons(ContentManager Content, Rectangle sceneRectangle)
        {
            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Textures/PlatformTilesTemp"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/tileFrame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties passableText = new DrawTextProperties("x", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);
            DrawTextProperties codeValueText = new DrawTextProperties("", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);
            Texture2D background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
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
                                   Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                        editorMapSquares[x, y].Draw(spriteBatch);

                    }
                }

            // base.Draw(spriteBatch);
        }

        #endregion

    }
}
