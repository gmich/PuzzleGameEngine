using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actors;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Utils;

namespace GateGame.Actors
{
    public class ActorManager
    {

        #region Declarations

        List<MapObject> mapObjects;
        List<StaticObject> staticObjects;
        List<Player> players;
        Enumerator playerEnumerator;
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
            players = new List<Player>();
            playerEnumerator = new Enumerator(players.Count, 0);
        }

        public void AddMapObject(MapObject mapObject)
        {
            mapObjects.Add(mapObject);
        }

        public void AddStaticObject(StaticObject staticObject)
        {
            staticObjects.Add(staticObject);                 
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            playerEnumerator.Count = players.Count;
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

        public bool HasActorAtLocation(Vector2 location,Player playerToCheck)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return true;
            }

            foreach (Player player in players)
            {
                if (player != playerToCheck)
                {
                    if (player.Intersects(location))
                        return true;
                }
            }

            return false;
        }

        public Gate GetInteractionGate(Rectangle otherRectangle)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject is Gate)
                {
                    Gate gate = (Gate)staticObject;
                    if (gate.InteractionRectangle.Intersects(otherRectangle))
                        return gate;
                }
            }
            return null;
        }

        public void IntersectsWithCoin(Rectangle otherRectangle)
        {
            for (int i = 0; i < staticObjects.Count; i++)
            {
                if (staticObjects[i] is Coin)
                {    
                    if (staticObjects[i].InteractionRectangle.Intersects(otherRectangle))
                    {
                        staticObjects.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public void InteractsWithHiddenWall(Rectangle otherRectangle,MapObject interactionActor)
        {
            for (int i = 0; i < staticObjects.Count; i++)
            {
                if (staticObjects[i] is HiddenWall)
                {
                    if (staticObjects[i].InteractionRectangle.Intersects(otherRectangle))
                    {
                        HiddenWall hWall = (HiddenWall)staticObjects[i];
                        hWall.InteractionActor = interactionActor;
                    }
                }
            }
        }

        public Button GetInteractionButton(Rectangle otherRectangle)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject is Button)
                {
                    Button button = (Button)staticObject;
                    if (button.InteractionRectangle.Intersects(otherRectangle))
                        return button;
                }
            }
            return null;
        }

        public void ToggleGatesWithTag(string tag)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject is Gate)
                {
                    Gate gate = (Gate)staticObject;
                    if (gate.Tag == tag)
                        gate.Toggle();
                }
            }
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

        public Player GetNextPlayer()
        {
            players[playerEnumerator.Value].IsActive = false;

            playerEnumerator.Next();
            players[playerEnumerator.Value].IsActive = true;
            return players[playerEnumerator.Value];
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            foreach (MapObject mapObject in mapObjects)
                mapObject.Update(gameTime);

            foreach (StaticObject staticObject in staticObjects)
                staticObject.Update(gameTime);

            foreach (Player player in players)
                player.HandleTransparency(gameTime);
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapObject mapObject in mapObjects)
                mapObject.Draw(spriteBatch);

            foreach (StaticObject staticObject in staticObjects)
                staticObject.Draw(spriteBatch);

            foreach (Player player in players)
                player.Draw(spriteBatch);
        }

        #endregion
    }
}
