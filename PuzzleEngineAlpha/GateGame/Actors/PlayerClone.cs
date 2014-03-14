using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Actors
{
    public class PlayerClone : MapObject
    {

        #region Declarations

        Queue<Vector2> Velocities;
        Queue<bool> interactions;
        const int queueLimit = 200;
        readonly ActorManager actorManager;

        #endregion

        #region Constructor

        public PlayerClone(ActorManager actorManager, PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, Vector2 location, ContentManager content,int frameWidth, int frameHeight, int collideWidth, int collideHeight)
            : base(tileMap, camera, location, frameWidth, frameHeight, collideWidth, collideHeight)
        {
            this.actorManager = actorManager;
            this.animations.Add("run", new PuzzleEngineAlpha.Animations.AnimationStrip(content.Load<Texture2D>(@"Textures/player"), frameWidth, "run"));
            currentAnimation = "run";
            this.actorManager = actorManager;

            Reset();
        }

        #endregion

        #region Clone Properties

        bool HaveToRecord
        {
            get
            {
                return (Velocities.Count < queueLimit);
            }
        }

        public bool IsAlive
        {
            get;
            set;
        }

        public bool Destroy
        {
            get;
            set;
        }

        public Rectangle InteractionRectangle
        {
            get;
            set;
        }

        public Player playerToRecord
        {
            get;
            set;
        }

        #endregion

        #region Helper Methods

        void Reset()
        {
            Velocities = new Queue<Vector2>();
            interactions = new Queue<bool>();
            IsAlive = false;
            Destroy = false;
            enabled = false;
            location = Vector2.Zero;
        }

        void Interact()
        {
            Button button = actorManager.GetInteractionButton(this.CollisionRectangle);

            if (button != null)
            {
                actorManager.ToggleGatesWithTag(button.Tag);
            }
        }
        #endregion

        #region Collision Detection

        public override void HorizontalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1,this) || actorManager.HasActorAtLocation(corner2,this))
            {
                moveAmount.X = 0.0f;
                velocity.X = 0.0f;
                Collided = true;
            }
        }

        public override void VerticalActorCollision(ref Vector2 moveAmount, Vector2 corner1, Vector2 corner2)
        {
            if (actorManager.HasActorAtLocation(corner1,this) || actorManager.HasActorAtLocation(corner2,this))
            {
                moveAmount.Y = 0.0f;
                velocity.Y = 0.0f;
                Collided = true;
            }
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            if (playerToRecord.CollisionRectangle.Intersects(this.InteractionRectangle))
            {
                Reset();
                return;
            }
            else if (!(playerToRecord.CollisionRectangle.Intersects(this.InteractionRectangle)) && HaveToRecord && !IsAlive)
            {
                if (this.location == Vector2.Zero)
                    location = playerToRecord.location;

                Velocities.Enqueue(playerToRecord.Velocity);
                interactions.Enqueue(playerToRecord.Interaction);

                if (!HaveToRecord)
                {
                    enabled = true;
                    IsAlive = true; 
                }
            }
            else if (IsAlive)
            {
                if (Velocities.Count > 0)
                {
                    this.Velocity = Velocities.Dequeue();
                    if (interactions.Dequeue())
                        Interact();
                }
                else
                {
                    Destroy = true;
                }

                actorManager.IntersectsWithCoin(this.CollisionRectangle);
                actorManager.InteractsWithHiddenWall(this.CollisionRectangle, this);

                base.Update(gameTime);

                //TODO: may not be necessary
               // if (Collided)
                  //  Destroy = true;
            }
        }

    }
}
