using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Actors
{
    public class HiddenWall : PuzzleEngineAlpha.Actors.StaticObject
    {
        #region Declarations

        PuzzleEngineAlpha.Animations.SmoothTransition tranparencyTransition;
        List<MapObject> InteractionActors;

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
            InteractionActors = new List<MapObject>();

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

        #region Helper Methods

        public void AddInteractionActor(MapObject mapObject)
        {
            if (InteractionActors != null)
            {
                if (!InteractionActors.Contains(mapObject))
                {
                    this.InteractionActors.Add(mapObject);
                }
            }
        }

        void HandleTransparency(GameTime gameTime)
        {
            if (Enabled)
                tranparencyTransition.Increase(gameTime);

            if (InteractionActors != null)
            {
                foreach (MapObject actor in InteractionActors)
                {
                    if (!actor.CollisionRectangle.Intersects(this.CollisionRectangle))
                    {
                        Enabled = true;
                        InteractionActors = null;
                    }
                }
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
