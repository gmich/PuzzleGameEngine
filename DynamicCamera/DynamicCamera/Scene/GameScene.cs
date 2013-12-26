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
            renderTarget = new RenderTarget2D(graphicsDevice, this.Width,this.Height); 
            player = new DummyPlayer(new Vector2(1000, 1000), content.Load<Texture2D>(@"player"));
            cameraScript = new ChasingCamera(player.location, new Vector2(this.Width, this.Height), new Vector2(50000,50000));
            cameraScript.AddCameraMan(new Rotater(0.0f, MathHelper.PiOver2, 10));
            tileMap = new TileMap(cameraScript.Camera.Position, cameraScript.Camera, content.Load<Texture2D>("PlatformTiles"), 50, 50);
            tileMap.Randomize(200, 200);
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
                return ResolutionHandler.WindowHeight - GameBanner.Height;
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

        public static Vector2 CameraLocation
        {
            get
            {
                return cameraScript.Camera.Position;
            }
        }

        #endregion

        #region Helper Methods

        void HandleZoom()
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                cameraScript.Camera.Zoom += ZoomStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                cameraScript.Camera.Zoom -= ZoomStep;
        }

        void HandleRotation()
        {
            if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                cameraScript.Camera.Rotation += RotationStep;

            else if (InputHandler.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                cameraScript.Camera.Rotation -= RotationStep;
        }

        #endregion

        #endregion

        //TODO: remove 
        Vector2 initialpos;
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
                    initialpos = InputHandler.MousePosition + cameraScript.Camera.Position;
                }

                cameraScript.TargetLocation = initialpos;
            }
            if (!InputHandler.LeftButtonIsClicked())
            {
                clicked = false;
                cameraScript.TargetLocation = player.Center;
            }
            cameraScript.Update(gameTime);
        }


        //TODO: fix rendertarget order
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cameraScript.Camera.GetTransformation(graphicsDevice));
            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
         //   spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                    BlendState.AlphaBlend,
                    null,
                    null,
                    null,
                    null,
                    cameraScript.Camera.GetTransformation(graphicsDevice));

            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
            //spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            spriteBatch.End();
        }
    }
}
