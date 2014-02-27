using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Level
{
    using Databases.Level;

    public class MapHandler
    {
        #region Declarations

        TileMap tileMap;
        Animations.IAnimation animation;
        ILevelInfoDB levelInfoDB;
        IMapDB mapDB;
        FileStream fileStream;
        Thread handlerThread;

        #endregion

        #region Constructor

        public MapHandler(ContentManager Content, TileMap tileMap, ILevelInfoDB levelInfoDB, IMapDB mapDB)
        {
            this.mapDB = mapDB;
            this.levelInfoDB = levelInfoDB;
            this.animation = new Animations.CircularAnimation(Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), 10.0f);
            this.tileMap = tileMap;
        }

        #endregion

        #region Properties

        bool IsActive
        {
            get
            {
                return handlerThread.IsAlive;
            }
        }

        #endregion

        #region Load/Save Map

        public void LoadMap()
        {
            LevelInfo levelInfo = levelInfoDB.Load(fileStream);
            tileMap.MapHeight = levelInfo.MapHeight;
            tileMap.MapWidth = levelInfo.MapWidth;
            tileMap.TileHeight = levelInfo.TileHeight;
            tileMap.TileWidth = levelInfo.TileWidth;

            tileMap.mapCells = new MapSquare[tileMap.MapWidth, tileMap.MapHeight];
            tileMap.mapCells = mapDB.Load(fileStream);
        }

        public void SaveMap()
        {
            mapDB.Save(fileStream, tileMap.mapCells);
            levelInfoDB.Save(fileStream, new LevelInfo(tileMap.MapWidth, tileMap.MapHeight, tileMap.TileWidth, tileMap.TileHeight));
        }

        #endregion

        #region Asynchronous Methods

        public void LoadMapAsynchronously(FileStream fileStream)
        {
            this.fileStream = fileStream;
            handlerThread = new Thread(this.LoadMap);
               
        }

        public void SaveMapAsynchronously(FileStream fileStream)
        {
            this.fileStream = fileStream;
            handlerThread = new Thread(this.SaveMap);

        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (this.IsActive)
            this.animation.Update(gameTime);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsActive)
            this.animation.Draw(spriteBatch);
        }
        #endregion

    }
}