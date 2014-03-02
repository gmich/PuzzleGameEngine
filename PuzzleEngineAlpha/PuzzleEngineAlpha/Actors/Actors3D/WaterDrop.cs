using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Actors.Actors3D
{
    class WaterDrop:Cube
    {
        float activeTime;
        float timePassed;

        public WaterDrop(Vector3 position, Vector3 size,float activeTime):base(position,size)
        {
            timePassed = 0.0f;
            this.activeTime = activeTime;
        }

        public bool IsAlive
        {
            get
            {
                return Position.Y <50;
            }
        }
        public void Update(GameTime gameTime)
        {
            timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            ConstructCube();
        }
      

    }
}
