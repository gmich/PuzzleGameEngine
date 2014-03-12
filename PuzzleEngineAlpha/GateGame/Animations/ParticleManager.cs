using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Animations
{
    class ParticleManager
    {
        #region Declarations

        List<Particle> particles;
        Texture2D particleTexture;
        Random rand;

        #endregion

        #region Constructor
        public ParticleManager(ContentManager Content)
        {
            this.particleTexture = Content.Load<Texture2D>(@"textures/whiteRectangle");
            particles = new List<Particle>();
            rand = new Random();
        }

        #endregion

        #region Private Helper Methods

        Vector2 RandomDirection(float scale)
        {
            Vector2 direction;
            do
            {
                direction = new Vector2(rand.Next(0, 100) - 50, rand.Next(0, 100) - 50);
            } while (direction.Length() == 0);

            direction.Normalize();
            direction *= scale;

            return direction;
        }

        #endregion

        #region Add Particles Methods

        public void AddSparksEffect(Vector2 location, int width, int height)
        {
            int particleCount = rand.Next(10, 20);
            for (int x = 0; x < particleCount; x++)
            {
                Particle particle = new Particle(location, particleTexture, new Rectangle(0, 0, width, height), RandomDirection((float)rand.Next(10, 20)), Vector2.Zero, 60, 20, Color.Yellow, Color.Orange);
                particles.Add(particle);
            }
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            for (int x = particles.Count - 1; x >= 0; x--)
            {
                particles[x].Update(gameTime);
                if (particles[x].Expired)
                {
                    particles.RemoveAt(x);
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }

        #endregion

    }
}