using System;
using System.Collections.Generic;
using PuzzleEngineAlpha.Level;
using Microsoft.Xna.Framework;

namespace PlatformerPrototype.AI
{
    class PathFilter
    {
        //TileMap tileMap;

        public static Vector2 FilterNextLocation(List<Vector2> directions)
        {
            if (directions != null)
            {
               
                if (directions.Count > 3)
                {
                    if ((directions[1].Y == directions[2].Y && directions[2].Y == directions[3].Y) &&( (directions[0].Y-1)==directions[1].Y) && directions[1].X<directions[2].X)
                    {
                        return new Vector2(directions[1].X -2, directions[1].Y);
                    }
                    if ((directions[1].Y == directions[2].Y && directions[2].Y == directions[3].Y) && ((directions[0].Y - 1) == directions[1].Y) && directions[1].X > directions[2].X)
                    {
                        return new Vector2(directions[1].X + 2, directions[1].Y);
                    }
       
                }
            
                return directions[1];
            }

            return Vector2.Zero;

        }
    }
}
