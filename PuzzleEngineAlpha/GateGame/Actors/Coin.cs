using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GateGame.Actors
{
    public class Coin : PuzzleEngineAlpha.Actors.StaticObject
    {
        #region Declarations

        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;

        #endregion

        #region Constructor

        public Coin(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, int frameWidth, int frameHeight,string tag)
            : base(tileMap,camera,location, frameWidth, frameHeight)
        {
            this.animations.Add("active", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "active"));
            currentAnimation = "active";
            this.enabled = false;
            this.Tag = tag;
            tranparencyTransition = new PuzzleEngineAlpha.Animations.SmoothTransition(1.0f, 0.0001f, 0.7f, 1.0f);
            ReduceTransparency = true;
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

        bool ReduceTransparency
        {
            get;
            set;
        }

        #endregion

        #region Helper Methods

        void HandleTransparency(GameTime gameTime)
        {
            if (ReduceTransparency)
                tranparencyTransition.Decrease(gameTime);
            else
                tranparencyTransition.Increase(gameTime);

            if (tranparencyTransition.Value == tranparencyTransition.MinValue)
                ReduceTransparency = false;
            else if (tranparencyTransition.Value == tranparencyTransition.MaxValue)
                ReduceTransparency = true;

            Transparency = tranparencyTransition.Value;
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            HandleTransparency(gameTime);

            base.Update(gameTime);
        }

        #endregion

    }
}
