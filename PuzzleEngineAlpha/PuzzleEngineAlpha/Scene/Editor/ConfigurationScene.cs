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
            Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion

        #region GUI Initialization

        void InitializeGUI(ContentManager Content)
        {
            textBox = new TextBox(Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), new Vector2(5, 5) + SceneLocation, 160, 30);

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), 0.9f, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), 0.8f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("passable", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, 1.0f, 1.0f);

            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(5, 50) + SceneLocation, new Vector2(160, 40),this.SceneRectangle));

            textProperties.text = "Tiles";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(0, 100) + SceneLocation, new Vector2(85, 60),this.SceneRectangle));

            textProperties.text = "Actors";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(85, 100) + SceneLocation, new Vector2(85, 60),this.SceneRectangle));
            
        }

        #endregion

        #region Handle Resolution Change

        public void ResetSizes(object sender, EventArgs e)
        {
            components[0].Position = new Vector2(5, 50) + SceneLocation;
            components[0].GeneralArea = SceneRectangle;

            components[1].Position = new Vector2(0, 100) + SceneLocation;
            components[1].GeneralArea = SceneRectangle;

            components[2].Position = new Vector2(85, 100) + SceneLocation;
            components[2].GeneralArea = SceneRectangle;

            textBox.Location = new Vector2(5, 5) + SceneLocation;
        }

        #endregion

        #region Properties

        Vector2 SceneLocation
        {
            get
            {
                return new Vector2(Resolution.ResolutionHandler.WindowWidth - scenerySize.X, 0);
            }
        }

        Rectangle SceneRectangle
        {
            get
            {
                return new Rectangle((int)this.SceneLocation.X, (int)this.SceneLocation.Y, (int)this.scenerySize.X, (int)this.scenerySize.Y);
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
            return;
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

            graphicsDevice.Clear(new Color(20,20,20));
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend);

            foreach (AGUIComponent component in components)
                component.Draw(spriteBatch);

            textBox.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
