using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actors;

namespace GateGame.Actors
{
    public class ActorManager
    {

        #region Declarations

        List<MapObject> mapObjects;
        List<StaticObject> staticObjects;

        #endregion

        #region Constructor

        public ActorManager()
        {
          Reset();
        }

        #endregion

        #region Add/Remove Actors

        public void Reset()
        {
            mapObjects = new List<MapObject>();
            staticObjects = new List<StaticObject>();
        }

        public void AddMapObject(MapObject mapObject)
        {
            mapObjects.Add(mapObject);
        }

        public void AddStaticObject(StaticObject staticObject)
        {
            staticObjects.Add(staticObject);
        }

        public bool HasActorAtLocation(Vector2 location)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return true;
            }
            return false;
        }

        public bool InteractsWithGate(Rectangle otherRectangle)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject is Gate)
                {
                    Gate gate = (Gate)staticObject;
                    if (gate.InteractionRectangle.Intersects(otherRectangle))
                        return true;
                }
            }
            return false;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (MapObject mapObject in mapObjects)
                mapObject.Update(gameTime);

            foreach (StaticObject staticObject in staticObjects)
                staticObject.Update(gameTime);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapObject mapObject in mapObjects)
                mapObject.Draw(spriteBatch);

            foreach (StaticObject staticObject in staticObjects)
                staticObject.Draw(spriteBatch);
        }

        #endregion
    }
}
