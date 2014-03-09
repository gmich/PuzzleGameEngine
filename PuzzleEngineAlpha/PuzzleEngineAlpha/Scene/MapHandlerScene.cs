using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene
{
    using Level;
    using Databases.Level;
    
    public class MapHandlerScene : IScene
    {
        #region Declarations

        TileMap tileMap;
        Animations.IAnimation animation;
        Animations.DisplayMessage displayMessage;
        ILevelInfoDB levelInfoDB;
        IMapDB mapDB;
        string path;
        Thread handlerThread;

        #endregion

        #region Constructor

        public MapHandlerScene(ContentManager Content, TileMap tileMap, ILevelInfoDB levelInfoDB, IMapDB mapDB)
        {
            this.mapDB = mapDB;
            this.levelInfoDB = levelInfoDB;
            this.animation = new Animations.CircularAnimation(Content.Load<SpriteFont>(@"Fonts/font"), 40.0f);
            this.tileMap = tileMap;
            displayMessage = new Animations.DisplayMessage(Content);
            displayMessage.OffSet=new Vector2(0, +145);
        }

        #endregion

        #region Properties

        public bool IsActive
        {
            get
            {

                if (handlerThread == null && !displayMessage.IsAlive)
                    return false;

                return true; 
            }
            set
            {}
        }


        bool ShowAnimation
        {
            get
            {
                if (handlerThread == null)
                    return false;

                return (handlerThread.IsAlive || !displayMessage.IsAlive);
            }
        }

        #endregion

        #region Load/Save Map

        public void LoadMap()
        {
            try
            {
                LevelInfo levelInfo = levelInfoDB.Load(path);
                this.tileMap.levelInfo = levelInfo;
                tileMap.MapHeight = levelInfo.MapHeight;
                tileMap.MapWidth = levelInfo.MapWidth;
                tileMap.TileHeight = levelInfo.TileHeight;
                tileMap.TileWidth = levelInfo.TileWidth;
                tileMap.mapCells = new MapSquare[tileMap.MapWidth, tileMap.MapHeight];
                tileMap.mapCells = mapDB.Load(path);
             //   displayMessage.StartAnimation("map successfully loaded", 1.5f);
              
            }
            catch (Exception ex)
            { 
                displayMessage.StartAnimation("loading failed :( ", 1.5f);
            }
            handlerThread = null; 
        }

        public void SaveMap()
        {
            try
            {
                mapDB.Save(path, tileMap.mapCells);
                levelInfoDB.Save(path, new LevelInfo(tileMap.MapWidth, tileMap.MapHeight, tileMap.TileWidth, tileMap.TileHeight));
                displayMessage.StartAnimation("map successfully saved", 1.5f);
            }
            catch (Exception ex)
            {
                displayMessage.StartAnimation("saving failed :( ", 1.5f);
            }
            handlerThread = null; 
        }

        public void GoInactive()
        {
            this.handlerThread.Abort();
        }
       
        #endregion

        #region Asynchronous Methods

        public void LoadMapAsynchronously(string path)
        {
            if (handlerThread == null)
            {
                this.path = path;
                handlerThread = new Thread(this.LoadMap);
                handlerThread.Start();
            }
        }

        public void SaveMapAsynchronously(string path)
        {
            if (handlerThread == null)
            {
                this.path = path;
                handlerThread = new Thread(this.SaveMap);
                handlerThread.Start();
            }

        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            this.displayMessage.Update(gameTime);

            if (ShowAnimation)
                this.animation.Update(gameTime);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (IsActive)
            {
                 if (ShowAnimation)
                this.animation.Draw(spriteBatch);

                this.displayMessage.Draw(spriteBatch);

            }

            spriteBatch.End();
        }

        #endregion

    }
}