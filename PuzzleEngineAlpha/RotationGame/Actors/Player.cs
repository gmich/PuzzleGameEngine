using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RotationGame.Actors
{
    using PuzzleEngineAlpha.Input.Scripts;
    using PuzzleEngineAlpha.Scene;

    public class Player : PuzzleEngineAlpha.Actors.MapObject
    {

        #region Constructor

        public Player(PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, Texture2D texture, float step, int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap,camera,location, frameWidth, frameHeight, collideWidth,collideHeight)
        {
            this.InitialLocation = location;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(texture, frameWidth, "run"));
            currentAnimation = "run";
            this.step = step;
        }

        #endregion

        #region Properties

        public Vector2 InitialLocation
        {
            get;
            set;
        }

        float step
        {
            get;
            set;
        }        

        public Vector2 Center
        {
            get
            {
                return new Vector2(location.X + collideWidth / 2, location.Y + collideHeight / 2);
            }
        }

        #endregion

        #region Collision Detection

        public override void HorizontalActorCollision(ref Vector2 moveAmount,Vector2 corner1, Vector2 corner2)
        {
            return;
        }

        public override void VerticalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            return;
        }

        #endregion

        #region Helper Methods

        public Vector2 GetVelocity(int state)
        {

            switch (state)
            {
                case 0:
                    return new Vector2(0, -step);
                case 2:
                    return new Vector2(0, +step);
                case 1:
                    return new Vector2(-step, 0);
                case 3:
                    return new Vector2(step, 0);
                default:
                    return Vector2.Zero;
            }


        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            velocity = GetVelocity(camera.RotationState);

            base.Update(gameTime);

            if (Collided)
                location = InitialLocation;
        }

    }
}
