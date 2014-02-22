using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Scene.Editor
{

    using Components;
    using Components.Buttons;
    using Components.TextBoxes;

    public class ConfigurationScene : IScene
    {
        #region Declarations

        RenderTarget2D renderTarget;
        bool isActive;
        GraphicsDevice graphicsDevice;
        Vector2 scenerySize;
        List<AGUIComponent> components;
        TextBox textBox;

        #endregion

        #region Constructor

        public ConfigurationScene(GraphicsDevice graphicsDevice, ContentManager Content, Vector2 scenerySize)
        {
            this.graphicsDevice = graphicsDevice;
            this.scenerySize = scenerySize;
            components = new List<AGUIComponent>();

            InitializeGUI(Content);
            UpdateRenderTarget();

        }

        #endregion

        #region GUI Initialization

        void InitializeGUI(ContentManager Content)
        {
            textBox = new TextBox(Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), new Vector2(5, 5), 160, 30);

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("passable", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(5, 50), new Vector2(160, 40)));

            textProperties.text = "Tiles";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(0,100), new Vector2(85, 60)));

            textProperties.text = "Actors";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(85, 100), new Vector2(85, 60)));
        }

        #endregion

        #region Properties

        Vector2 SceneLocation
        {
            get
            {
                return Vector2.Zero;
            }
        }

        #region Boundaries

        int Width
        {
            get
            {
                return (int)scenerySize.X;
            }
        }

        int Height
        {
            get
            {
                return (int)scenerySize.Y;
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
            renderTarget = new RenderTarget2D(graphicsDevice,this.Width, this.Height);
        }


        #endregion


        public void Update(GameTime gameTime)
        {
            foreach (AGUIComponent component in components)
                component.Update(gameTime);

            textBox.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend);

            foreach (AGUIComponent component in components)
                component.Draw(spriteBatch);

            textBox.Draw(spriteBatch);

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(renderTarget, SceneLocation, Color.White);

            spriteBatch.End();
        }
    }
}
