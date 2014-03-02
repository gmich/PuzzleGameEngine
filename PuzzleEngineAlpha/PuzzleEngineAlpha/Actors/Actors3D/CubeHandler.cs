using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Actors.Actors3D
{
    class CubeHandler
    {
        GraphicsDevice device;
        BasicEffect cubeEffect;
        Cube cube;
        float rotation;
        Vector3 cameraPosition;
        Vector3 modelPosition;
        Texture2D cubeTexture;
        float aspectRatio;

        public CubeHandler(GraphicsDevice device,ContentManager Content)
        {
            this.device = device;
            cubeEffect = new BasicEffect(device);
            cube = new Cube(new Vector3(0,0,0),new Vector3(3,3,3));
            rotation = 0.0f;
            cameraPosition = new Vector3(0, 0, 2);
            modelPosition = new Vector3(0, 0, 0);
            cubeTexture = Content.Load<Texture2D>("Textures/rect3d");
            aspectRatio = 4.0f / 3.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            device.Clear(Color.CornflowerBlue);

            // Set the World matrix which defines the position of the cube
            cubeEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(rotation)) *
                Matrix.CreateRotationX(MathHelper.ToRadians(rotation)) * Matrix.CreateTranslation(modelPosition);

            cubeEffect.View = Matrix.CreateLookAt(cameraPosition, modelPosition, Vector3.Backward);

            // Set the Projection matrix which defines how we see the scene (Field of view)
            cubeEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f);

            cubeEffect.TextureEnabled = true;
            cubeEffect.Texture = cubeTexture;

            // Enable some pretty lights
            cubeEffect.EnableDefaultLighting();
            // apply the effect and render the cube
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                cube.RenderToDevice(device);
            }

        }
    }
}
