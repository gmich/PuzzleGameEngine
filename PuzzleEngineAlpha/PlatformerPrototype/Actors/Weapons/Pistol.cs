using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PlatformerPrototype.Actors.Weapons
{
    using Animations;
    using Actors.Handlers;

    public class Pistol : IWeapon
    {
        #region Declarations

        List<Bullet> bullets;
        ActorManager actorManager;
        ParticleManager particleManager;
        PuzzleEngineAlpha.Camera.Camera camera;
        PuzzleEngineAlpha.Level.TileMap tileMap;
        ContentManager content;
        int collideWidth;
        int collideHeight;
        
        #region Bullet Constants
        
        readonly float step = 500.0f;
        readonly Vector2 memento = new Vector2(0, 5);

        #endregion
        
        #endregion

        #region Constructor

        public Pistol(ActorManager actorManager, ParticleManager particleManager, PuzzleEngineAlpha.Level.TileMap tileMap, PuzzleEngineAlpha.Camera.Camera camera, ContentManager content)
        {
            this.camera = camera;
            this.bullets = new List<Bullet>();
            this.actorManager = actorManager;
            this.particleManager = particleManager;
            this.tileMap= tileMap;
            this.content = content;
            this.collideWidth = 5;
            this.collideHeight = 5;
            this.actorManager = actorManager;
        }

        #endregion

        #region Shoot

        public void Shoot(Vector2 location, Vector2 velocity)
        {
            Bullet bullet = new Bullet(actorManager, particleManager, tileMap, camera, location, velocity * this.step, memento, content, collideWidth, collideHeight, collideWidth, collideHeight);
            bullets.Add(bullet);
            actorManager.AddMapObject(bullet);
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            for(int i=0;i<bullets.Count;i++)
            {
                bullets[i].Update(gameTime);
                if (bullets[i].Destroy)
                {
                    particleManager.AddRectangleDestructionParticles(Color.DarkBlue,bullets[i].location, this.collideWidth, collideWidth, 1, 1);
                    actorManager.RemoveMapObject(bullets[i]);
                    bullets.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
        }

        #endregion
    }
}
