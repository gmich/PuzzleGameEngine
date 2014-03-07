using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace PuzzleEngineAlpha.Level
{
    using Databases.Level;

    public class MiniMap : TileMap
    {
        #region Declarations

        Dictionary<string, Texture2D> miniMaps;
        string[] maps;

        string activeMiniMap;
        string previousMiniMap;
        Scene.Editor.MapHandlerScene mapHandler;
        GraphicsDevice graphicsDevice;
        SpriteBatch minimapBatch;
        Texture2D background;
        Texture2D actors;
        Animations.DisplayMessage message;

        #endregion

        #region Constructor

        public MiniMap(ContentManager Content, GraphicsDevice graphicsDevice, Vector2 size, IMapDB mapDB, ILevelInfoDB levelInfoDB)
            : base(Vector2.Zero, Content, 64, 64, 64, 64)
        {
            this.Size = size;
            minimapBatch = new SpriteBatch(graphicsDevice);
            this.graphicsDevice = graphicsDevice;
            mapHandler = new Scene.Editor.MapHandlerScene(Content, this, levelInfoDB, mapDB);
            this.Camera = new Camera.Camera(Vector2.Zero, size, size);
            miniMaps = new Dictionary<string, Texture2D>();
            activeMiniMap = null;
            previousMiniMap = null;
            message = new Animations.DisplayMessage(Content);
            CurrentMapID = 0;
            background = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            actors = Content.Load<Texture2D>(@"Textures/ActorsTemp");
            message.OffSet = new Vector2(0, 300);
        }

        #endregion

        #region Get Maps

        public void LoadMaps()
        {
            maps = Parsers.DBPathParser.GetMapNames();
        }

        #endregion

        #region Public Helper Methods

        public void Refresh()
        {
            miniMaps = new Dictionary<string, Texture2D>();
            maps = null;
            CurrentMapID = CurrentMapID;
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

                if (maps == null || maps.Length == 0)
                {
                    message.StartAnimation("no saved maps", -1.0f);
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

                previousMiniMap = activeMiniMap;
                activeMiniMap = maps[currentMapID];
                message.StartAnimation(MapTitle, -1.0f);
            }
        }

        public bool HasLoadedMap
        {
            get
            {
                return (maps.Length > 0 && currentMapID > -1);
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

        string MapTitle
        {
            get
            {
                if (maps == null || maps.Length == 0)
                    return "no saved maps";
                else
                    return maps[currentMapID] + " " + (CurrentMapID + 1) + "/" + maps.Length;
            }
        }

        Rectangle MinimapScreenRectangle
        {
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y);
            }
        }

        Rectangle FrameRectangle
        {
            get
            {
                int offSet = 1;
                return new Rectangle((int)Location.X - offSet, (int)Location.Y - offSet, (int)Size.X + offSet * 2, (int)Size.Y + offSet);
            }
        }

        Vector2 Location
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth / 2 - Size.X / 2, Resolution.ResolutionHandler.WindowHeight / 2 - Size.Y +30);
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

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(miniMapRenderTarget);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    spriteBatch.Draw(tileSheet, CellScreenRectangle(x, y), TileSourceRectangle(mapCells[x, y].LayerTile),
                      GetColor(CellScreenRectangle(x, y)), 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

                    if (mapCells[x, y].ActorID > -1)
                        spriteBatch.Draw(actors, CellScreenRectangle(x, y), TileSourceRectangle(mapCells[x, y].ActorID),
                        GetColor(CellScreenRectangle(x, y)), 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

                }
            }
            spriteBatch.End();

            miniMaps.Add(map, (Texture2D)miniMapRenderTarget);
            activeMiniMap = map;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            message.Draw(spriteBatch);

            spriteBatch.Draw(background, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap - 0.01f);

            if (activeMiniMap == null || mapHandler.IsActive)
            {
                if (previousMiniMap != null)
                {
                    spriteBatch.Draw(background, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap - 0.01f);
                    if (miniMaps.ContainsKey(previousMiniMap))
                    {
                        spriteBatch.Draw(miniMaps[previousMiniMap], MinimapScreenRectangle ,null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap);
                    }
                }

                if (maps == null)
                {
                    spriteBatch.Draw(background, FrameRectangle, null, Color.Black, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap - 0.01f);
                    spriteBatch.Draw(background, MinimapScreenRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap);
                }
                return;
            }

            if (currentMapID != -1)
            {
                if (miniMaps.ContainsKey(activeMiniMap))
                {
                    spriteBatch.Draw(miniMaps[activeMiniMap], MinimapScreenRectangle, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, Scene.DisplayLayer.MiniMap);
                }
            }

            DrawMinimap(spriteBatch, activeMiniMap);
        }

        #endregion
    }
}
