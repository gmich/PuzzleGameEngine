using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
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

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    editorMapSquares[x, y] = new EditorMapSquare(button, frame, new Vector2(x * TileWidth, y * TileHeight), new Vector2(TileWidth, TileHeight), TileSourceRectangle(mapCells[x, y].LayerTile), this.Camera, sceneRectangle);
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

        /*  public override Color GetColor(Rectangle rectangle)
          {
              if (InputHandler.MouseRectangle.Intersects(rectangle))
              {
                  if (InputHandler.LeftButtonIsClicked())
                      return new Color(200, 200, 200);
                  else
                      return new Color(220, 220, 220);
              }
              else
                  return Color.White;
          }*/

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
            Scene.Game.DiagnosticsScene.SetText(new Vector2(5, 30), "Location: {X: " + StartX + " / " + MapWidth + "  Y: " + StartY + " / " + MapHeight + "}");
            Scene.Game.DiagnosticsScene.SetText(new Vector2(5, 55), "Scale: " + Camera.Zoom);

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
