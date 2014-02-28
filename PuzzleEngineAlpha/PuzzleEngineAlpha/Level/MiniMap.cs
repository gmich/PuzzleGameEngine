using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace PuzzleEngineAlpha.Level
{
    using Databases.Level;

    public class MiniMap :TileMap
    {
        #region Declarations

        Dictionary<string, Texture2D> miniMaps;
        string[] maps;

        string activeMiniMap;
        Scene.Editor.MapHandlerScene mapHandler;
        GraphicsDevice graphicsDevice;
        Texture2D background;

        #endregion

        #region Constructor

        public MiniMap(ContentManager Content,GraphicsDevice graphicsDevice,Vector2 size,IMapDB mapDB,ILevelInfoDB levelInfoDB):base(Vector2.Zero,Content,64,64,64,64)
        {
            this.Size = size;
            this.graphicsDevice = graphicsDevice;
            mapHandler = new Scene.Editor.MapHandlerScene(Content,this,levelInfoDB, mapDB);
            this.Camera = new Camera.Camera(Vector2.Zero, size, size);
            miniMaps = new Dictionary<string, Texture2D>();
            activeMiniMap = null;
            currentMapID=-1;
            LoadMaps();
            background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
        }

        #endregion

        #region Get Maps

        public void LoadMaps()
        {
            maps = Parsers.DBPathParser.GetMapNames();
        }

        #endregion

        #region Properties

        int currentMapID;
        public int CurrentMapID
        {
            get
            {
                return currentMapID;
            }
            set
            {
                LoadMaps();

                if (maps == null)
                {
                    currentMapID = -1;
                    return;
                }
                if (value > maps.Length - 1)
                    currentMapID = 0;
                else if (value < 0)
                    currentMapID = maps.Length - 1;
                else
                    currentMapID = value;

                mapHandler.LoadMapAsynchronously(Parsers.DBPathParser.GetMapNameFromPath(maps[currentMapID]));
                activeMiniMap = maps[currentMapID];
            }
        }

        public override int TileWidth
        {
            get
            {
                return (int)MathHelper.Max(1, (Size.X / MapWidth));
            }
            set { }
        }

        public override int TileHeight
        {
            get
            {
                return (int)MathHelper.Max(1, (Size.Y / MapHeight));
            }
            set { }
        }

        Rectangle MinimapScreenRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        Vector2 Location
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y-100);
            }
        }

        Vector2 Size
        {
            get;
            set;
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            mapHandler.Update(gameTime);
        }

        #region Draw

        void DrawMinimap(SpriteBatch spriteBatch, string map)
        {
            if (miniMaps.ContainsKey(map)) return;

            RenderTarget2D miniMapRenderTarget = new RenderTarget2D(graphicsDevice, (int)Size.X, (int)Size.Y);
            graphicsDevice.SetRenderTarget(miniMapRenderTarget);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    spriteBatch.Draw(tileSheet, CellScreenRectangle(x, y), TileSourceRectangle(mapCells[x, y].LayerTile),
                      GetColor(CellScreenRectangle(x, y)), 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            miniMaps.Add(map, (Texture2D)miniMapRenderTarget);
            activeMiniMap = map;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
   
            mapHandler.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(background, MinimapScreenRectangle, Color.White);

            spriteBatch.End();

            if (activeMiniMap == null || mapHandler.IsActive)
            {
                return;
            }

            DrawMinimap(spriteBatch, activeMiniMap);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            if (currentMapID!=-1)
            {
                if (miniMaps.ContainsKey(activeMiniMap))
                    spriteBatch.Draw(miniMaps[activeMiniMap], MinimapScreenRectangle, Color.White);
            }
            spriteBatch.End();
        }

        #endregion
    }
}
