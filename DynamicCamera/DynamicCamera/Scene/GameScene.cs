using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DynamicCamera.Scene
{
    using Input;
    using Level;

    public class GameScene : IScene
    {
        #region Declarations

        static ICameraScript cameraScript;
        RenderTarget2D renderTarget;
        bool isActive;
        DummyPlayer player;
        TileMap tileMap;
        GraphicsDevice graphicsDevice;

        #endregion

        #region Constructor

        public GameScene(GraphicsDevice graphicsDevice,ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            player = new DummyPlayer(new Vector2(1000, 1000), content.Load<Texture2D>(@"player"),10);
            cameraScript = new ChasingCamera(player.location, new Vector2(this.Width, this.Height), new Vector2(50000,50000));
            Rotater rotater = new Rotater(0.0f, MathHelper.PiOver2, 10);
            freeroam = new DummyPlayer(player.location, null, 15);
            rotater.Triggered += CameraRotated;
            cameraScript.AddCameraMan(rotater);
           
            tileMap = new TileMap(cameraScript.Camera.Position, cameraScript.Camera, content.Load<Texture2D>("PlatformTiles"), 64,64);
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

        #region Event Handling

        private void CameraRotated(object sender, EventArgs e)
        {
            RotationArgs args = (RotationArgs)e;

            //TODO: input configuration should be handled elsewhere
            player.InitializeKeys(args.RotationState);
            freeroam.InitializeKeys(args.RotationState);
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

        public static Vector2 CameraLocation
        {
            get
            {
                return cameraScript.Camera.Position;
            }
        }

        #endregion

        #region Helper Methods

        public void UpdateRenderTarget()
        {
            renderTarget = new RenderTarget2D(graphicsDevice, this.Width, this.Height);
            cameraScript.Camera.ViewPortWidth = ResolutionHandler.WindowWidth;
            cameraScript.Camera.ViewPortHeight = ResolutionHandler.WindowHeight;
        }

        //Encapsulate this in a cameraMan object
        void HandleZoom()
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                cameraScript.Camera.Zoom += ZoomStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                cameraScript.Camera.Zoom -= ZoomStep;
        }

        #endregion

        #endregion

        #region Update

        //TODO: remove 
        bool clicked = false;
        DummyPlayer freeroam;
        public void Update(GameTime gameTime)
        {
            HandleZoom();
            
            //cameraScript.TargetLocation = player.Center;

            //TODO: encapsulate this in an ICameraMan object
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)) 
            {
                if (!clicked)
                {
                    clicked = true;
                    freeroam.location = player.location;
                }
                freeroam.Update(gameTime);
                cameraScript.TargetLocation = freeroam.location;
            }
            else
            {
                clicked = false;
                player.Update(gameTime);
                cameraScript.TargetLocation = player.Center;
            }
            cameraScript.Update(gameTime);
        }

        #endregion

        #region Draw

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
                        cameraScript.Camera.GetTransformation(graphicsDevice));

            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            WindowText.Draw(spriteBatch);

            spriteBatch.End();
        }

        #endregion
    }
}
