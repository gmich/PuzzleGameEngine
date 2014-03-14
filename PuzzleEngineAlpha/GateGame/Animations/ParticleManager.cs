using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Actors;
using PuzzleEngineAlpha.Camera;

namespace GateGame.Animations
{
    public class ParticleManager
    {
        #region Declarations

        List<Particle> particles;
        Texture2D particleTexture;
        Random rand;
        Camera camera;
        #endregion

        #region Constructor
        public ParticleManager(ContentManager Content,Camera camera)
        {
            this.particleTexture = Content.Load<Texture2D>(@"Textures/whiteRectangle");
            particles = new List<Particle>();
            rand = new Random();
            this.camera = camera;
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

        Vector2 RandomLocation(int offSet)
        {
            return new Vector2(rand.Next(-offSet, +offSet), rand.Next(-offSet, +offSet));
        }

        #endregion

        #region Add Particles Methods

        public void AddCloneParticles(Vector2 location, int width, int height)
        {
            int particleCount = rand.Next(10, 20);
            for (int x = 0; x < particleCount; x++)
            {
                Particle particle = new Particle(location, particleTexture, new Rectangle(0, 0, width, height), RandomDirection((float)rand.Next(10, 20)), Vector2.Zero, 60, 20, Color.Yellow, Color.Orange);
                particle.Camera = this.camera;
                particles.Add(particle);
            }
        }

        public void AddRectangleDestructionParticles(Vector2 location, int rectWidth, int rectHeight, int particleWidth, int particleHeight)
        {
            for (int x = 0; x < rectWidth / particleWidth; x++)
            {
                for (int y = 0; y < rectHeight / particleHeight; y++)
                {
                    Particle particle = new Particle(location + new Vector2(x * particleWidth, y * particleHeight), particleTexture, new Rectangle(0, 0, particleWidth, particleHeight), RandomDirection((float)rand.Next(10, 20)), RandomDirection((float)rand.Next(10, 20)), 70, 70, Color.Black, Color.White);
                    particle.Camera = this.camera;
                    particles.Add(particle);
                }
            }
        }

        public void AddRecordingParticles(Vector2 location,int amount, int width, int height,int duration)
        {
            for (int x = 0; x < amount; x++)
            {
                Vector2 particleLocation = location + RandomLocation(9);
                Particle particle = new Particle(particleLocation, particleTexture, new Rectangle(0, 0, width, height), RandomDirection((float)rand.Next(10, 20)), RandomDirection((float)rand.Next(10, 20)), 70, duration, Color.Black, Color.White);
                particle.Camera = this.camera;
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