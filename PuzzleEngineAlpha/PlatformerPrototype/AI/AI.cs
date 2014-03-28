using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PlatformerPrototype.AI
{
    public interface AI
    {
        List<Vector2> FindPath(Vector2 startTile, Vector2 endTile);
    }
}
