using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Actors
{
    public class HiddenWall : PuzzleEngineAlpha.Actors.StaticObject
    {
        #region Declarations

        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;

        #endregion

        #region Constructor

        public HiddenWall(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, int frameWidth, int frameHeight, string tag)
            : base(tileMap,camera,location, frameWidth, frameHeight)
        {
            this.animations.Add("active", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "active"));
            currentAnimation = "active";
            this.enabled = false;
            this.Tag = tag;
            tranparencyTransition = new PuzzleEngineAlpha.Animations.SmoothTransition(0.1f, 0.002f, 0.1f, 1.0f);
            Transparency = tranparencyTransition.Value;
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

        public MapObject InteractionActor
        {
            get;
            set;
        }
        
        #endregion

        #region Helper Methods

        void HandleTransparency(GameTime gameTime)
        {
            if (Enabled)
                tranparencyTransition.Increase(gameTime);

            if (InteractionActor != null)
            {
                if (!InteractionActor.CollisionRectangle.Intersects(this.CollisionRectangle))
                    Enabled = true;

                InteractionActor = null;
            }

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
