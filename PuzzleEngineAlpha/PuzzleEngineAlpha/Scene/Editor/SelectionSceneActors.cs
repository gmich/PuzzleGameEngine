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

    public class SelectionSceneActors : SelectionScene
    {
        #region Constructor

        public SelectionSceneActors(GraphicsDevice graphicsDevice, ContentManager Content, int TileWidth, int TileHeight, Vector2 scenerySize)
            : base(graphicsDevice, Content, TileWidth, TileHeight, scenerySize)
        {
            this.IsActive = false;
            this.DrawScene = false;
            tileSheet = Content.Load<Texture2D>(@"Textures/ActorsTemp");
            components = new List<AGUIComponent>();
            InitializeGUI(Content);
        }
        
        #endregion

        #region GUI Initialization

        public override void InitializeGUI(ContentManager Content)
        {
            camera = new Camera.Camera(Vector2.Zero, new Vector2(this.Width, Resolution.ResolutionHandler.WindowHeight - 215), new Vector2(this.Width, this.Height));

            for (int i = 0; i < CountTiles; i++)
            {
                DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Textures/ActorsTemp"), 0.9f, 1.0f, 0.0f, Color.White);
                DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/tileFrame"), 0.8f, 1.0f, 0.0f, Color.White);
                components.Add(new TileButton(button, frame, new Vector2((i % 2) * (TileWidth + TileOffset) + TileOffset, i / 2 * (TileHeight + TileOffset) + TileOffset) + SceneLocation, new Vector2(TileWidth, TileHeight), TileSourceRectangle(i), this.camera, this.SceneRectangle, i));
                components[i].StoreAndExecuteOnMouseRelease(new Actions.SetSelectedTileAction((TileButton)components[i]));
            }
            vScrollBar = new VScrollBar(Content.Load<Texture2D>(@"ScrollBars/bullet"), Content.Load<Texture2D>(@"ScrollBars/bar"), camera, new Vector2(this.scenerySize.X - ScrollBarWidth, 0) + SceneLocation, new Vector2(ScrollBarWidth, scenerySize.Y - 5), Scene.DisplayLayer.Editor + 0.1f);
        }

        #endregion

    }
}
