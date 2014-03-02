using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors.Actors3D
{
    class Cube
    {
        bool isConstructed;
        int NUMvertices = 36;
        int NUM_TRIANGLES = 36;
        VertexPositionNormalTexture[] vertices;

        Vector3 Position
        {
            get;
            set;
        }

        Vector3 Size
        {
            get;
            set;
        }

        public Cube(Vector3 position, Vector3 size)
        {
            this.Position = position;
            this.Size = size;
        }
        
        public void RenderToDevice(GraphicsDevice device)
        {
            if (isConstructed == false)
                ConstructCube();

            using (VertexBuffer buffer = new VertexBuffer(
                device,
               VertexPositionNormalTexture.VertexDeclaration,
              NUMvertices,
              BufferUsage.WriteOnly))
            {
                device.SetVertexBuffer(buffer);
            }

            // Draw the primitives from the vertex buffer to the device as triangles
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);
        }

        private void ConstructCube()
        {
            vertices = new VertexPositionNormalTexture[NUMvertices];

            // Calculate the position of the vertices on the top face.
            Vector3 topLeftFront = Position + new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 topLeftBack = Position + new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightFront = Position + new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 topRightBack = Position + new Vector3(1.0f, 1.0f, 1.0f) * Size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = Position + new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmLeftBack = Position + new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 btmRightFront = Position + new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmRightBack = Position + new Vector3(1.0f, -1.0f, 1.0f) * Size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f) * Size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f) * Size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * Size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * Size;
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f) * Size;
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f) * Size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(1.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureBottomLeft = new Vector2(1.0f * Size.X, 1.0f * Size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * Size.X, 1.0f * Size.Y);

            // Add the vertices for the FRONT face.
            vertices[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);
            vertices[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            vertices[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
            vertices[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            vertices[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);
            vertices[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);

            // Add the vertices for the BACK face.
            vertices[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            vertices[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            vertices[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            vertices[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            vertices[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            vertices[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            vertices[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            vertices[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);
            vertices[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);
            vertices[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            vertices[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);
            vertices[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            // Add the vertices for the BOTTOM face. 
            vertices[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            vertices[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);
            vertices[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            vertices[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            vertices[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            vertices[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);

            // Add the vertices for the LEFT face.
            vertices[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);
            vertices[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            vertices[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);
            vertices[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);
            vertices[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            vertices[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            // Add the vertices for the RIGHT face. 
            vertices[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            vertices[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);
            vertices[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            vertices[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);
            vertices[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            vertices[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);

            isConstructed = true;
        }
       
        
      

    }
}
