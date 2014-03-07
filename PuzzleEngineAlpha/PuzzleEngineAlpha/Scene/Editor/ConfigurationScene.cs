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
        static TextBox textBox;

        #endregion

        #region Constructor

        public ConfigurationScene(GraphicsDevice graphicsDevice, ContentManager Content, Vector2 scenerySize,IScene selectionScene,IScene actorSelectionScene)
        {
            Level.Editor.TileManager.ShowActors = false;
            Level.Editor.TileManager.MapSquare = null;
            Level.Editor.TileManager.TileSheet = Content.Load<Texture2D>(@"Textures/PlatformTilesTemp");
            Level.Editor.TileManager.ActorTileSheet = Content.Load<Texture2D>(@"Textures/ActorsTemp");
            this.graphicsDevice = graphicsDevice;
            this.scenerySize = scenerySize;
            components = new List<AGUIComponent>();
            isActive = true;
            InitializeGUI(Content, selectionScene, actorSelectionScene);
            UpdateRenderTarget();
            Resolution.ResolutionHandler.Changed += ResetSizes;
        }

        #endregion

        #region GUI Initialization

        void InitializeGUI(ContentManager Content, IScene selectionScene, IScene actorSelectionScene)
        {
            Passable = true;
            textBox = new TextBox(new Input.KeyboardInput(),Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Content.Load<Texture2D>(@"Textboxes/textbox"), new Vector2(5, 5) + SceneLocation, 160, 30, Scene.DisplayLayer.Editor - 0.01f);
            textBox.StoreAndExecuteOnTextChange(new Actions.SetSelectedCodeValueAction(textBox));

            DrawProperties button = new DrawProperties(Content.Load<Texture2D>(@"Buttons/button"), Scene.DisplayLayer.Editor, 1.0f, 0.0f, Color.White);
            DrawProperties frame = new DrawProperties(Content.Load<Texture2D>(@"Buttons/frame"), Scene.DisplayLayer.Editor + 0.02f, 1.0f, 0.0f, Color.White);
            DrawProperties clickedButton = new DrawProperties(Content.Load<Texture2D>(@"Buttons/clickedButton"), Scene.DisplayLayer.Editor + 0.01f, 1.0f, 0.0f, Color.White);
            DrawTextProperties textProperties = new DrawTextProperties("is passable", 11, Content.Load<SpriteFont>(@"Fonts/menuButtonFont"), Color.Black, Scene.DisplayLayer.Editor + 0.03f, 1.0f);

            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(5, 50) + SceneLocation, new Vector2(160, 40),this.SceneRectangle));
            components[0].StoreAndExecuteOnMouseRelease(new Actions.TogglePassableAction((MenuButton)components[0]));

            textProperties.text = "unselect";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(5, 100) + SceneLocation, new Vector2(160, 40), this.SceneRectangle));
            components[1].StoreAndExecuteOnMouseRelease(new Actions.UnSelectedTileAction());

            textProperties.text = "tiles";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(0, 150) + SceneLocation, new Vector2(85, 60),this.SceneRectangle));
            components[2].StoreAndExecuteOnMouseRelease(new Actions.ToggleSelectionAction((SelectionScene)selectionScene, (SelectionScene)actorSelectionScene, true));

            textProperties.text = "actors";
            components.Add(new MenuButton(button, frame, clickedButton, textProperties, new Vector2(85, 150) + SceneLocation, new Vector2(85, 60),this.SceneRectangle));
            components[3].StoreAndExecuteOnMouseRelease(new Actions.ToggleSelectionAction((SelectionScene)selectionScene, (SelectionScene)actorSelectionScene, false));
        }

        #endregion

        #region Public Static Information

        public static bool Passable
        {
            get;
            set;
        }

        public static string TextBoxText
        {
            get
            {
                return textBox.Text;
            }
        }
        #endregion

        #region Handle Resolution Change

        public void ResetSizes(object sender, EventArgs e)
        {
            components[0].Position = new Vector2(5, 50) + SceneLocation;
            components[0].GeneralArea = SceneRectangle;

            components[1].Position = new Vector2(5, 100) + SceneLocation;
            components[1].GeneralArea = SceneRectangle;

            components[2].Position = new Vector2(0, 150) + SceneLocation;
            components[2].GeneralArea = SceneRectangle;

            components[3].Position = new Vector2(85, 150) + SceneLocation;
            components[3].GeneralArea = SceneRectangle;

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

            textBox.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend);

            foreach (AGUIComponent component in components)
                component.Draw(spriteBatch);

            textBox.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
