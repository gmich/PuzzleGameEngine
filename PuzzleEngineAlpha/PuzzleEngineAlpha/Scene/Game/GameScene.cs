using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Game
{
    using Input;
    using Level;
    using Player;
    using Camera.Scripts;
    using Camera.Handlers;
    using Camera.Managers;
    using Resolution;

    public class GameScene : IScene
    {
        #region Declarations

        RenderTarget2D renderTarget;
        CameraManager cameraManager;
        bool isActive;
        DummyPlayer player;
        TileMap tileMap;
        GraphicsDevice graphicsDevice;
        Camera.Camera camera;
        Vector2 sceneryOffSet;
        #endregion

        #region Constructor

        public GameScene(GraphicsDevice graphicsDevice, ContentManager content, Vector2 sceneryOffSet)
        {
            this.graphicsDevice = graphicsDevice;
            this.sceneryOffSet = sceneryOffSet;
            player = new DummyPlayer(new Vector2(1000, 1000), content.Load<Texture2D>(@"Textures/player"),10);
            cameraManager = new CameraManager();
            camera = new Camera.Camera( player.location,new Vector2(this.Width, this.Height), new Vector2(50000, 50000));
            cameraManager.SetCameraScript(new ChasingCamera(player.location,camera));
            cameraManager.AddCameraHandler(new Rotater(0.0f, MathHelper.PiOver2, 10));
            cameraManager.AddCameraHandler(new Zoomer(1.0f, 1.0f, 0.01f, 0.6f));
            player.Camera = cameraManager.Camera;
            tileMap = new TileMap(cameraManager.Position, content, 64, 64,64,64);
            tileMap.Camera = cameraManager.Camera;
            tileMap.Randomize(200, 200);
            UpdateRenderTarget();
            this.sceneryOffSet = sceneryOffSet;
        }

        #endregion
        
        #region Properties

        Vector2 SceneLocation
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        #region Boundaries

        int Width
        {
            get
            {
                return ResolutionHandler.WindowWidth - (int)sceneryOffSet.X;
            }
        }

        int Height
        {
            get
            {
                return ResolutionHandler.WindowHeight - (int)sceneryOffSet.Y;
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

        #region Camera Related

        #region Properties

        public Vector2 CameraLocation
        {
            get
            {
                return cameraManager.Camera.Position;
            }
        }

        #endregion

        #region Helper Methods

        public void UpdateRenderTarget()
        {
            renderTarget = new RenderTarget2D(graphicsDevice, this.Width, this.Height);
            cameraManager.Camera.ViewPortWidth = this.Width;
            cameraManager.Camera.ViewPortHeight = this.Height;
        }

        #endregion

        #endregion

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            cameraManager.TargetLocation = player.RelativeCenter;
            cameraManager.Update(gameTime);
        }

        //TODO: fix rendertarget order
        public void Draw(SpriteBatch spriteBatch)
        {

            graphicsDevice.Clear(Color.CornflowerBlue);
            graphicsDevice.SetRenderTarget(renderTarget);
            
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.PointWrap,
                        null,
                        null,
                        null,
                        cameraManager.Camera.GetTransformation());

            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            spriteBatch.End();
        }
    }
}
