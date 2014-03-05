using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Editor
{

    using Components;
    using Components.Buttons;
    using Components.ScrollBars;

    public class SelectionScene : IScene
    {
        #region Declarations

        bool isActive;
        GraphicsDevice graphicsDevice;
        protected Vector2 scenerySize;
        protected List<AGUIComponent> components;
        protected Texture2D tileSheet;
        protected Camera.Camera camera;
        protected VScrollBar vScrollBar;

        #endregion

        #region Constructor

        public SelectionScene(GraphicsDevice graphicsDevice, ContentManager Content,int TileWidth, int TileHeight, Vector2 scenerySize)
        {
            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;
            tileSheet = Content.Load<Texture2D>(@"Textures/PlatformTilesTemp");
            this.graphicsDevice = graphicsDevice;
            this.scenerySize = scenerySize;
            components = new List<AGUIComponent>();
            InitializeGUI(Content);
            UpdateRenderTarget();
            Resolution.ResolutionHandler.Changed += ResetSizes;
            isActive = true;
            this.DrawScene = true;

        }

        #endregion

        #region Handle Resolution Change

        public void ResetSizes(object sender, EventArgs e)
        {
            this.scenerySize = new Vector2(170, Resolution.ResolutionHandler.WindowHeight - 215);

            vScrollBar.Size = new Vector2(ScrollBarWidth,  scenerySize.Y-5);

            this.camera.ViewPortHeight = (int)(Resolution.ResolutionHandler.WindowHeight - SceneLocation.Y);
            
            for(int i=0;i<components.Count;i++)
            {
                components[i].GeneralArea = SceneRectangle;
                components[i].Position = new Vector2((i % 2) * (TileWidth + TileOffset) + TileOffset, i / 2 * (TileHeight + TileOffset) + TileOffset) + SceneLocation;
            }

            vScrollBar.BarLocation = new Vector2(this.scenerySize.X - ScrollBarWidth,0) + SceneLocation;
            vScrollBar.bulletLocation = new Vector2(this.scenerySize.X - ScrollBarWidth + SceneLocation.X, vScrollBar.BulletLocation.Y);

        }

        #endregion

        #region Information about Tiles

        protected int TileWidth
        {
            get;
            set;
        }

        protected int TileHeight
        {
            get;
            set;
        }

        int TilesPerRow
        {
            get { return tileSheet.Width / TileWidth; }
        }

        int TilesPerColumn
        {
            get { return tileSheet.Height / TileHeight; }
        }

        protected Rectangle TileSourceRectangle(int tileIndex)
        {
            return new Rectangle(
                (tileIndex % TilesPerRow) * TileWidth,
                (tileIndex / TilesPerRow) * TileHeight,
                TileWidth,
                TileHeight);
        }

        protected int CountTiles
        {
            get
            {
                return (TilesPerRow * TilesPerColumn);
            }
        }


        #endregion

        #region GUI Initialization

        public virtual void InitializeGUI(ContentManager Content)
        {
            camera = new Camera.Camera(Vector2.Zero, new Vector2(this.Width, Resolution.ResolutionHandler.WindowHeight - 215), new Vector2(this.Width, this.Height));
          
            for (int i = 0; i < CountTiles; i++)
            {
                DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Textures/PlatformTilesTemp"), Scene.DisplayLayer.Editor + 0.1f, 1.0f, 0.0f, Color.White);
                DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/tileFrame"), Scene.DisplayLayer.Editor + 0.2f, 1.0f, 0.0f, Color.White);
                components.Add(new TileButton(button, frame, new Vector2((i % 2) * (TileWidth+TileOffset) + TileOffset, i / 2 * (TileHeight + TileOffset) + TileOffset) + SceneLocation, new Vector2(TileWidth, TileHeight),TileSourceRectangle(i),this.camera,this.SceneRectangle,i));
                components[i].StoreAndExecuteOnMouseRelease(new Actions.SetSelectedTileAction((TileButton)components[i]));
            }
            vScrollBar = new VScrollBar(Content.Load<Texture2D>(@"ScrollBars/bullet"), Content.Load<Texture2D>(@"ScrollBars/bar"), camera, new Vector2(this.scenerySize.X - ScrollBarWidth, 0) + SceneLocation, new Vector2(ScrollBarWidth, scenerySize.Y-5),Scene.DisplayLayer.Editor+0.1f);
        }

        #endregion

        #region Properties

        protected float ScrollBarWidth
        {
            get
            {
                return 20.0f;
            }
        }

        protected Vector2 SceneLocation
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth - 170,215);
            }
        }

        #region Boundaries

        protected int TileOffset
        {
            get
            {
                return 8;
            }
        }

        protected int Width
        {
            get
            {
                return (TileWidth + TileOffset) * 2;
            }
        }

        protected int Height
        {
            get
            {
                return (int)(MathHelper.Max((Resolution.ResolutionHandler.WindowHeight-SceneLocation.Y),(CountTiles+1) * (TileHeight + TileOffset) / 2) + TileOffset);
            }
        }

       protected Rectangle SceneRectangle
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

        public bool DrawScene
        {
            get;
            set;
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
            foreach (AGUIComponent component in components)
                component.Update(gameTime);

            vScrollBar.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {

            if (!this.DrawScene) return;

            RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, rasterizerState);

            foreach (AGUIComponent component in components)
            {
              //  if (camera.inScreenBounds(component.Position))
                    component.Draw(spriteBatch);
            }

            vScrollBar.Draw(spriteBatch);

            spriteBatch.GraphicsDevice.ScissorRectangle = SceneRectangle;

            spriteBatch.End();

       
        }
    }
}
