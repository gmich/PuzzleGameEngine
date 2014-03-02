using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Actors.Actors3D
{
    public class Actor3D
    {
        #region Declarations

        Rectangle3D rect3D;
        VertexDeclaration vertexDeclaration;
        Matrix View, Projection;
        GraphicsDevice graphicsDevice;
        Texture2D texture;
        BasicEffect quadEffect;

        #endregion

        #region Constructor

        public Actor3D(GraphicsDevice graphicsDevice,ContentManager Content)
        {
            this.graphicsDevice = graphicsDevice;
            rect3D = new Rectangle3D(Vector3.Zero, Vector3.Backward, Vector3.Up, 1, 1);
            View = Matrix.CreateLookAt(new Vector3(0, 0, 2), Vector3.Zero,
                Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 4.0f / 3.0f, 1, 500);

            texture = Content.Load<Texture2D>("Textures/rect3d");

            InitializeQuadEffect();
        }

        #endregion

        void InitializeQuadEffect()
        {
            quadEffect = new BasicEffect(graphicsDevice);
            quadEffect.EnableDefaultLighting();

            quadEffect.World = Matrix.Identity;
            quadEffect.View = View;
            quadEffect.Projection = Projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = texture;
            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
       }

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    rect3D.Vertices, 0, 4,
                    rect3D.Indexes, 0, 2);
            }

        }

        #endregion

    }
}

