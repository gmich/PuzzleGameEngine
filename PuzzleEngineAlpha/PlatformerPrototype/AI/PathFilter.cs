using System;
using System.Collections.Generic;
using PuzzleEngineAlpha.Level;
using PuzzleEngineAlpha.Actors;
using Microsoft.Xna.Framework;

namespace PlatformerPrototype.AI
{
    class PathFilter
    {

        #region Declarations

        TileMap tileMap;
        MapObject mob;

        #endregion

        #region Constructor

        public PathFilter(TileMap tileMap, MapObject mob)
        {
            this.tileMap = tileMap;
            this.mob = mob;
        }

        #endregion

        public Vector2 FilterNextLocation(List<Vector2> directions)
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

        public Vector2 FilterVelocity(List<Vector2> directions, float timePassed)
        {
            if (directions != null)
            {
                if (CanReachNextLocation(directions[0], directions[1], timePassed))
                {
                   return directions[1];
                }
            }
            return new Vector2(directions[1].X+1, directions[1].Y-1);
        }

        bool CanReachNextLocation(Vector2 currentMapCell, Vector2 nextMapCell,float timePassed)
        {
            Vector2 nextLocation = mob.location + (mob.Velocity * 0.01f);
            return (currentMapCell != tileMap.GetCellByPixel(nextLocation));
                
        }
    }
}
