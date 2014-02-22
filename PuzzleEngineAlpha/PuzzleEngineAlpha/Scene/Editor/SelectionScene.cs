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
        Vector2 scenerySize;
        List<AGUIComponent> components;
        Texture2D tileSheet;
        Camera.Camera camera;
        VScrollBar vScrollBar;
        #endregion

        #region Constructor

        public SelectionScene(GraphicsDevice graphicsDevice, ContentManager Content,int TileWidth, int TileHeight,Vector2 sceneLocation, Vector2 scenerySize)
        {
            this.TileWidth = TileWidth;
            this.TileHeight = TileHeight;
            this.SceneLocation = sceneLocation;
            tileSheet = Content.Load<Texture2D>(@"Textures/PlatformTilesTemp");
            this.graphicsDevice = graphicsDevice;
            this.scenerySize = scenerySize;
            components = new List<AGUIComponent>();
            InitializeGUI(Content);
            UpdateRenderTarget();
        }

        #endregion

        #region Information about Tiles

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

        int TilesPerRow
        {
            get { return tileSheet.Width / TileWidth; }
        }

        int TilesPerColumn
        {
            get { return tileSheet.Height / TileHeight; }
        }

        Rectangle TileSourceRectangle(int tileIndex)
        {
            return new Rectangle(
                (tileIndex % TilesPerRow) * TileWidth,
                (tileIndex / TilesPerRow) * TileHeight,
                TileWidth,
                TileHeight);
        }

        int CountTiles
        {
            get
            {
                return (TilesPerRow * TilesPerColumn);
            }
        }


        #endregion

        #region GUI Initialization

        void InitializeGUI(ContentManager Content)
        {
            camera = new Camera.Camera(Vector2.Zero, new Vector2(this.Width, Resolution.ResolutionHandler.WindowHeight - 160), new Vector2(this.Width, this.Height));
          
            for (int i = 0; i < CountTiles; i++)
            {
                DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Textures/PlatformTilesTemp"), 0.9f, 1.0f, 0.0f, Color.White);
                DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/tileFrame"), 0.8f, 1.0f, 0.0f, Color.White);
                components.Add(new TileButton(button, frame, new Vector2((i % 2) * (TileWidth+TileOffset) + TileOffset, i / 2 * (TileHeight + TileOffset) + TileOffset) + SceneLocation, new Vector2(TileWidth, TileHeight),TileSourceRectangle(i),this.camera));
            }
           float scrollBarWidth = 20.0f;
           vScrollBar = new VScrollBar(Content.Load<Texture2D>(@"ScrollBars/bullet"), Content.Load<Texture2D>(@"ScrollBars/bar"), camera, new Vector2(this.scenerySize.X - scrollBarWidth, 0) + SceneLocation, new Vector2(scrollBarWidth, Resolution.ResolutionHandler.WindowHeight-SceneLocation.Y));
        }

        #endregion

        #region Properties

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
                return (TileWidth + TileOffset) * 2;
            }
        }

        int Height
        {
            get
            {
                return (int)(MathHelper.Max((Resolution.ResolutionHandler.WindowHeight-SceneLocation.Y),(CountTiles+1) * (TileHeight + TileOffset) / 2) + TileOffset);
            }
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

            RasterizerState rasterizerState = new RasterizerState() { ScissorTestEnable = true };
            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, rasterizerState);


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
