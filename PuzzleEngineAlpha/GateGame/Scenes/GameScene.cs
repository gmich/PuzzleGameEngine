using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha;
using PuzzleEngineAlpha.Input;
using PuzzleEngineAlpha.Level;
using PuzzleEngineAlpha.Actors;
using PuzzleEngineAlpha.Camera.Scripts;
using PuzzleEngineAlpha.Camera.Handlers;
using PuzzleEngineAlpha.Camera.Managers;
using PuzzleEngineAlpha.Resolution;
using PuzzleEngineAlpha.Camera;

namespace GateGame.Scene
{
    using Actors;
    using Animations;

    public class GameScene : PuzzleEngineAlpha.Scene.IScene
    {
        #region Declarations

        RenderTarget2D renderTarget;
        CameraManager cameraManager;
        bool isActive;
        Actors.Player player;
        TileMap tileMap;
        GraphicsDevice graphicsDevice;
        PuzzleEngineAlpha.Camera.Camera camera;
        Vector2 sceneryOffSet;
        Actors.ActorManager actorManager;
        ContentManager content;
        ActorMapper gateMapper;
        ChasingCamera chasingCamera;
        FreeRoam freeRoam;
        ParticleManager particleManager;

        #endregion

        #region Constructor

        public GameScene(GraphicsDevice graphicsDevice, ContentManager content,TileMap tileMap, Vector2 sceneryOffSet)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.sceneryOffSet = sceneryOffSet;
            cameraManager = new CameraManager();
            camera = new Camera( Vector2.Zero,new Vector2(this.Width, this.Height), new Vector2(50000, 50000));
            this.tileMap = tileMap;
            this.tileMap.Camera = camera;
            actorManager = new Actors.ActorManager();
            gateMapper = new ActorMapper(content,this.tileMap);
            actorManager.Reset();
            player = new Actors.Player(actorManager,tileMap, camera, new Vector2(-100, -100), content.Load<Texture2D>(@"Textures/player"), 25.0f, 16, 16, 15, 15);
            chasingCamera = new ChasingCamera(player.location, camera,2.0f);
            cameraManager.SetCameraScript(chasingCamera);
            IsCameraFree = false;

            //cameraManager.AddCameraHandler(new Rotater(0.0f, MathHelper.PiOver2, 8));
            cameraManager.AddCameraHandler(new Zoomer(1.0f, 1.0f, 0.5f, 0.01f));

            UpdateRenderTarget();
            this.sceneryOffSet = sceneryOffSet;
            PuzzleEngineAlpha.Resolution.ResolutionHandler.Changed += ResetSizes;
            this.tileMap.NewMap += NewMapHandling;
            particleManager = new ParticleManager(content,this.camera);
        }

        #endregion

        #region Handle New Map

        void NewMapHandling(object sender, EventArgs e)
        {
            actorManager.Reset();
            List<Vector2> playerLocations = tileMap.GetLocationOfCodeValue("player");

            if (playerLocations.Count>0)
            {
                foreach (Vector2 playerLocation in playerLocations)
                {
                    player = new Actors.Player(actorManager, tileMap, camera, playerLocation + new Vector2(16, 16), content.Load<Texture2D>(@"Textures/player"), 25.0f, 16, 16, 15, 15);
                    actorManager.AddPlayer(player);
                }
            }
            else
            {
                player = new Actors.Player(actorManager, tileMap, camera, new Vector2(16, 16), content.Load<Texture2D>(@"Textures/player"), 25.0f, 16, 16, 15, 15);
                actorManager.AddPlayer(player);
            }

            this.player = actorManager.GetNextPlayer();

            Dictionary<Vector2, int> actors = tileMap.GetActorsLocationAndID();

            foreach (KeyValuePair<Vector2, int> actor in actors)
            {
                StaticObject obj;
                if (actor.Value <= gateMapper.LastGateID)
                {
                    obj = new Gate(this.tileMap, this.camera, actor.Key, gateMapper.GetTextureByID(actor.Value), tileMap.SourceTileWidth, tileMap.SourceTileHeight, gateMapper.GetTagByID(actor.Value), gateMapper.IsGateEnabled(actor.Value));
                }
                else if (actor.Value == gateMapper.CoinID)
                {
                    obj = new Coin(this.tileMap, this.camera, actor.Key, gateMapper.GetTextureByID(actor.Value), tileMap.SourceTileWidth, tileMap.SourceTileHeight, gateMapper.GetTagByID(actor.Value));
                }
                else if (actor.Value == gateMapper.HiddenWallID)
                {
                    obj = new HiddenWall(this.tileMap, this.camera, actor.Key, gateMapper.GetTextureByID(actor.Value), tileMap.SourceTileWidth, tileMap.SourceTileHeight, gateMapper.GetTagByID(actor.Value));
                }
                else if (actor.Value == gateMapper.CloneBoxID)
                {
                    obj = new CloneBox(this.actorManager,this.particleManager,this.tileMap, this.camera, actor.Key,gateMapper.GetTextureByID(actor.Value), content,tileMap.SourceTileWidth, tileMap.SourceTileHeight, gateMapper.GetTagByID(actor.Value));
                }
                else
                {
                    obj = new Button(this.tileMap, this.camera, actor.Key, gateMapper.GetTextureByID(actor.Value), tileMap.SourceTileWidth, tileMap.SourceTileHeight, gateMapper.GetTagByID(actor.Value));
                }
                obj.CollisionRectangle = gateMapper.GetCollisionRectangleByID(actor.Value, actor.Key);
                obj.InteractionRectangle = gateMapper.GetInteractionRectangleByID(actor.Value, actor.Key);

                actorManager.AddStaticObject(obj);
            }
        }

        #endregion

        #region Handle Resolution Change

        void ResetSizes(object sender, EventArgs e)
        {
            UpdateRenderTarget();
            camera.ViewPortWidth = (int)this.Width;
            camera.ViewPortHeight = (int)this.Height;
        }

        #endregion    

        public void GoInactive()
        {
            this.IsActive = false;
        }

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

        bool IsCameraFree
        {
            get;
            set;
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

        void ToggleCameraScripts()
        {
            if(InputHandler.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                if (IsCameraFree)
                {
                    cameraManager.SetCameraScript(chasingCamera);
                    IsCameraFree = false;
                  
                }
                else
                {
                    freeRoam = new FreeRoam(player.RelativeCenter, this.camera);
                    cameraManager.SetCameraScript(freeRoam);
                    IsCameraFree = true;
                }
                UpdateDiagnostics();
            }

        }

        void UpdateDiagnostics()
        {
            string activeCam;

            if (IsCameraFree)
            {
                activeCam = "free roam";
            }
            else
            {
                activeCam = "player";
            }

            DiagnosticsScene.SetText(new Vector2(5, 25), "camera target: " + activeCam);
        }

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

            if (PuzzleEngineAlpha.Input.InputHandler.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {
                player = actorManager.GetNextPlayer();
            }

            ToggleCameraScripts();

            if(!IsCameraFree)
            player.Update(gameTime);

            particleManager.Update(gameTime);
            actorManager.Update(gameTime);
            cameraManager.TargetLocation = player.RelativeCenter;
            cameraManager.Update(gameTime);
        }

        //TODO: fix rendertarget order
        public void Draw(SpriteBatch spriteBatch)
        {

            graphicsDevice.Clear(new Color(20, 20, 20)) ;
      //      graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        SamplerState.PointWrap,
                        null,
                        null,
                        null,
                        cameraManager.Camera.GetTransformation());

            tileMap.Draw(spriteBatch);
            actorManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            particleManager.Draw(spriteBatch);
            spriteBatch.End();

           /* graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            spriteBatch.End();*/
        }
    }
}
