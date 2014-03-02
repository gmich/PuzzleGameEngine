using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Actors.Actors3D
{
    class Rain
    {
        GraphicsDevice device;
        BasicEffect effect;
        List<WaterDrop> drops;
        float rotation;
        Vector3 cameraPosition;
        Vector3 modelPosition;
        Texture2D cubeTexture;
        float aspectRatio;
        const int MaxWaterDrops = 500;

        public Rain(GraphicsDevice device, ContentManager Content)
        {
            drops = new List<WaterDrop>();
            this.device = device;
            effect = new BasicEffect(device);

            rotation = 0.0f;
            cameraPosition = new Vector3(5, -20, 0);
            modelPosition = new Vector3(1, 0, 0);
            cubeTexture = Content.Load<Texture2D>("Textures/rect3d");
            aspectRatio = 3.0f / 3.0f;
        }

        public void Update(GameTime gameTime)
        {
            float step = 70f;
            ManageWaterDrops();
            foreach (WaterDrop drop in drops)
            {
                drop.Update(gameTime);
                drop.Position = new Vector3(drop.Position.X, drop.Position.Y + step * (float)gameTime.ElapsedGameTime.TotalSeconds, drop.Position.Z);
            }
            for(int i=0;i<drops.Count;i++)
            {
                if (!drops[i].IsAlive)
                   drops.RemoveAt(i);
            }
        }

        void ManageWaterDrops()
        {
            Random rand = new Random();
            while (drops.Count < MaxWaterDrops)
            {
                drops.Add(new WaterDrop(new Vector3((float)rand.Next(-200, 200) * 0.1f, (float)rand.Next(-50, 0), (float)rand.Next(-2000, 2000) * 0.1f), new Vector3(0.05f, 0.05f, 0.05f), 25.0f));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            effect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) *
               Matrix.CreateRotationX(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(modelPosition);

            effect.View = Matrix.CreateLookAt(cameraPosition, modelPosition, new Vector3(0, 1, 0));
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 500.0f);

            effect.TextureEnabled = true;
            effect.Texture = cubeTexture;

            //  cubeEffect.EnableDefaultLighting();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                foreach (WaterDrop drop in drops)
                    drop.RenderToDevice(device);
            }

        }
    }
}
