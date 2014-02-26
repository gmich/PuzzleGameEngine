using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Editor
{
    using Level.Editor;
    using Components;
    using Components.Buttons;
    using Components.ScrollBars;

    public class MapScene : IScene
    {
        #region Declarations

        bool isActive;
        GraphicsDevice graphicsDevice;
        Vector2 scenerySize;
        Camera.Camera camera;
        Camera.Managers.CameraManager cameraManager;
        EditorTileMap tileMap;

        #endregion

        #region Constructor

        public MapScene(GraphicsDevice graphicsDevice, ContentManager Content, int TileWidth, int TileHeight, Vector2 sceneLocation, Vector2 scenerySize)
        {
            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;
            tileMap = new EditorTileMap(Vector2.Zero, Content,64,64, TileWidth, TileHeight, true);

            //TODO: remove after debugging
            tileMap.Randomize(100, 100);
            camera = new Camera.Camera(Vector2.Zero, scenerySize, new Vector2(this.Width, this.Height));
            cameraManager = new Camera.Managers.CameraManager();
            cameraManager.SetCameraScript(new Camera.Scripts.MouseCamera(camera));
            cameraManager.AddCameraHandler(new Camera.Handlers.Zoomer(1.0f,1.5f,0.6f,0.01f));
            tileMap.Camera = camera;

            this.SceneLocation = sceneLocation;
            this.graphicsDevice = graphicsDevice;
            this.scenerySize = scenerySize;
            UpdateRenderTarget();
            tileMap.InitializeButtons(Content,this.SceneRectangle);
            isActive = true;
            Resolution.ResolutionHandler.Changed += ResetSizes;

        }

        #endregion

        #region Handle Resolution Change

        public void ResetSizes(object sender, EventArgs e)
        {
            this.scenerySize = new Vector2(Resolution.ResolutionHandler.WindowWidth - 170, Resolution.ResolutionHandler.WindowHeight);
            this.tileMap.HandleResolutionChange(SceneRectangle);
            camera.ViewPortWidth = (int)this.scenerySize.X;
            camera.ViewPortHeight = (int)this.scenerySize.Y;
        }

        #endregion

        #region Properties

        int TileWidth
        {
            get;
            set;
        }

        int TileHeight
        {
            get;
            set;
        }

        float ScrollBarWidth
        {
            get
            {
                return 20.0f;
            }
        }

        Vector2 SceneLocation
        {
            get;
            set;
        }

        #region Boundaries

        int TileOffset
        {
            get
            {
                return 8;
            }
        }

        int Width
        {
            get
            {
                return tileMap.MapPixelWidth;
            }
        }

        int Height
        {
            get
            {
                return tileMap.MapPixelHeight;
            }
        }

        //TODO: implement
        int CountTiles
        {
            get { return 1; }
        }

    
        Rectangle SceneRectangle
        {
            get
            {
                return new Rectangle((int)this.SceneLocation.X, (int)this.SceneLocation.Y, (int)this.scenerySize.X, (int)this.scenerySize.Y);
            }
        }

        #endregion
        
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        #endregion

        #region Helper Methods

        public void GoInactive()
        {
            this.IsActive = false;

        }
        public void UpdateRenderTarget()
        {
            return;
        }


        #endregion
        
        public void Update(GameTime gameTime)
        {
            //vScrollBar.Update(gameTime);
            tileMap.Update(gameTime);
            cameraManager.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {

            RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap, null, rasterizerState, null, cameraManager.Camera.GetTransformation( ));
            
            tileMap.Draw(spriteBatch);

            spriteBatch.GraphicsDevice.ScissorRectangle = SceneRectangle;

            spriteBatch.End();
                   
        }
    }
}
