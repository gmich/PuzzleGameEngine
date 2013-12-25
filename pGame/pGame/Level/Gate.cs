using System;
using Microsoft.Xna.Framework;

namespace PuzzlePrototype.Level
{
    public class Gate
    {
        public GateStatus gateStatus;
        public bool passable;

        public Gate(GateStatus gateStatus, bool passable)
        {
            this.gateStatus = gateStatus;
            this.passable = passable;
        }

        public void ToggleState();

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime);

    }
}
