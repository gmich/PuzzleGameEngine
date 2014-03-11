using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GateGame.Actors
{
    public class Gate : PuzzleEngineAlpha.Actors.StaticObject
    {
        #region Declarations

        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;

        #endregion
        #region Constructor

        public Gate(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, int frameWidth, int frameHeight)
            : base(tileMap,camera,location, frameWidth, frameHeight)
        {
            this.animations.Add("active", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "active"));
            currentAnimation = "active";
            tranparencyTransition = new PuzzleEngineAlpha.Animations.SmoothTransition(1.0f, 0.004f, 0.2f, 1.0f);
        }

        #endregion

        #region Properties

        public string Tag
        {
            get;
            set;
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        #endregion

        #region Public Helper Methods

        public void Toggle()
        {
            enabled = !enabled;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (enabled)
                tranparencyTransition.Increase(gameTime);
            else
                tranparencyTransition.Decrease(gameTime);

            Transparency = tranparencyTransition.Value;
        }

        #endregion

    }
}
