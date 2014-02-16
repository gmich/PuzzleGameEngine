using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene
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

       // static ICameraScript cameraScript;
        RenderTarget2D renderTarget;
        CameraManager cameraManager;
        bool isActive;
        DummyPlayer player;
        DummyPlayer freeroam;
        TileMap tileMap;
        GraphicsDevice graphicsDevice;

        #endregion

        #region Constructor

        public GameScene(GraphicsDevice graphicsDevice,ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            player = new DummyPlayer(new Vector2(1000, 1000), content.Load<Texture2D>(@"player"),10);
            cameraManager = new CameraManager();
            cameraManager.SetCameraScript(new ChasingCamera(player.location, new Vector2(this.Width, this.Height), new Vector2(50000, 50000)));
            cameraManager.AddCameraHandler(new Rotater(0.0f, MathHelper.PiOver2, 10));
            tileMap = new TileMap(cameraManager.Position, cameraManager.Camera, content.Load<Texture2D>("PlatformTiles"), 64, 64);
            tileMap.Randomize(200, 200);
            UpdateRenderTarget();
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
                return ResolutionHandler.WindowWidth;
            }
        }

        int Height
        {
            get
            {
                return ResolutionHandler.WindowHeight ;
            }
        }

        #endregion
        
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion


        #region Camera Related

        #region Properties

        float ZoomStep
        {
            get
            {
                return 0.01f;
            }
        }

        float RotationStep
        {
            get
            {
                return 0.01f;
            }
        }

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
            cameraManager.Camera.ViewPortWidth = ResolutionHandler.WindowWidth;
            cameraManager.Camera.ViewPortHeight = ResolutionHandler.WindowHeight;
        }

        //encapsulate in a camera handler
        void HandleZoom()
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                cameraManager.Camera.Zoom += ZoomStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                cameraManager.Camera.Zoom -= ZoomStep;
        }

        #endregion

        #endregion

        //TODO: remove 
        bool clicked = false;
        public void Update(GameTime gameTime)
        {
            HandleZoom();
            player.Update(gameTime);
            //cameraScript.TargetLocation = player.Center;

            //TODO: encapsulate this in an ICameraMan object
            if (InputHandler.LeftButtonIsClicked())
            {
                if (!clicked)
                {
                    clicked = true;

                    freeroam.location = player.location;

                 //   initialpos = InputHandler.MousePosition + cameraScript.Camera.Position;

                }

              //  cameraScript.TargetLocation = initialpos;
            }
            if (!InputHandler.LeftButtonIsClicked())
            {
                clicked = false;
                cameraManager.TargetLocation = player.Center;
            }
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
                        cameraManager.Camera.GetTransformation(graphicsDevice));

            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            PuzzleEngineAlpha.Diagnostics.WindowText.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
