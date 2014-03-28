using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleEngineAlpha.Actors;
using Microsoft.Xna.Framework.Content;
using PuzzleEngineAlpha.Utils;

namespace PlatformerPrototype.Actors.Handlers
{
    public class ActorManager
    {

        #region Declarations

        List<MapObject> mapObjects;
        List<StaticObject> staticObjects;
        List<Player> players;
        List<PlayerClone> playerClones;
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
            playerClones = new List<PlayerClone>();
            playerEnumerator = new Enumerator(players.Count, 0);
        }

        public void AddMapObject(MapObject mapObject)
        {
            mapObjects.Add(mapObject);
        }

        public void AddPlayerClone(PlayerClone playerClone)
        {
            playerClones.Add(playerClone);
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

        public void RemovePlayerClone(PlayerClone clone)
        {
            for(int i=0;i<playerClones.Count;i++)
            {
                if (playerClones[i]==clone)
                {
                    playerClones.RemoveAt(i);
                    return;
                }
            }
        }

        #endregion

        #region Actor Interactions

        public bool HasActorAtLocation(Vector2 location)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return true;
            }
            foreach (Player player in players)
            {
                if (player.Intersects(location))
                    return true;
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone.Intersects(location))
                    return true;
            }

            return false;
        }

        public bool HasActorAtLocation(Vector2 location, MapObject mapObject)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return true;
            }

            foreach (Player player in players)
            {
                if (player != mapObject)
                {
                    if (player.Intersects(location))
                        return true;
                }
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone != mapObject)
                {
                    if (clone.Intersects(location))
                        return true;
                }
            }
            return false;
        }      

        #region Information About Actor Location and Boundaries

        public Vector2 GetActorLocation(Vector2 location)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return new Vector2(staticObject.CollisionRectangle.X, staticObject.CollisionRectangle.Y);
            }

            foreach (Player player in players)
            {
                if (player.Intersects(location))
                    return player.location;
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone.Intersects(location))
                    return clone.location;
            }

            return Vector2.Zero;
        }

        public Vector2 GetActorLocation(Vector2 location, MapObject mapObject)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return new Vector2(staticObject.CollisionRectangle.X, staticObject.CollisionRectangle.Y);
            }

            foreach (Player player in players)
            {
                if (player != mapObject)
                {
                    if (player.Intersects(location))
                        return player.location;
                }
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone != mapObject)
                {
                    if (clone.Intersects(location))
                        return clone.location;
                }
            }

            return Vector2.Zero;
        }

        public float GetActorHeight(Vector2 location, MapObject mapObject)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return staticObject.CollisionRectangle.Height;
            }

            foreach (Player player in players)
            {
                if (player != mapObject)
                {
                    if (player.Intersects(location))
                        return player.CollisionRectangle.Height+1;
                }
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone != mapObject)
                {
                    if (clone.Intersects(location))
                        return clone.CollisionRectangle.Height + 1;
                }
            }

            return 0.0f;
        }

      
        public float GetActorHeight(Vector2 location)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return staticObject.CollisionRectangle.Height;
            }

            foreach (Player player in players)
            {
                if (player.Intersects(location))
                    return player.CollisionRectangle.Height + 1;
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone.Intersects(location))
                    return clone.CollisionRectangle.Height + 1;

            }

            return 0.0f;
        }


        public float GetActorWidth(Vector2 location)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return staticObject.CollisionRectangle.Width;
            }

            foreach (Player player in players)
            {
                    if (player.Intersects(location))
                        return player.CollisionRectangle.Width + 1;

            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone.Intersects(location))
                    return clone.CollisionRectangle.Width + 1;
            }

            return 0.0f;
        }

        public float GetActorWidth(Vector2 location, MapObject mapObject)
        {
            foreach (StaticObject staticObject in staticObjects)
            {
                if (staticObject.Intersects(location))
                    return staticObject.CollisionRectangle.Width;
            }

            foreach (Player player in players)
            {
                if (player != mapObject)
                {
                    if (player.Intersects(location))
                        return player.CollisionRectangle.Width + 1;
                }
            }

            foreach (PlayerClone clone in playerClones)
            {
                if (clone != mapObject)
                {
                    if (clone.Intersects(location))
                        return clone.CollisionRectangle.Width + 1;
                }
            }

            return 0.0f;
        }

        #endregion

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
                        staticObjects[i].Remove = true;
                        return;
                    }
                }
            }
        }

        public void IntersectsWithCloneBox(Rectangle otherRectangle,Player interactionPlayer)
        {
            for (int i = 0; i < staticObjects.Count; i++)
            {
                if (staticObjects[i] is CloneBox)
                {
                    if (staticObjects[i].InteractionRectangle.Intersects(otherRectangle))
                    {
                        CloneBox cloneBox = (CloneBox)staticObjects[i];
                        cloneBox.AddPlayer(interactionPlayer);
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
                        hWall.AddInteractionActor(interactionActor);
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
                player.Update(gameTime);
        }

        public void MovePlayers(GameTime gameTime, bool freeRoam)
        {
            if(!freeRoam)
            foreach (Player player in players)
                player.Move();
        }
        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MapObject mapObject in mapObjects)
                mapObject.Draw(spriteBatch);

            for (int i = 0; i < staticObjects.Count; i++)
            {
                if (staticObjects[i].Remove)
                    staticObjects.RemoveAt(i);

                staticObjects[i].Draw(spriteBatch);
            }

            foreach (Player player in players)
                player.Draw(spriteBatch);

            foreach (PlayerClone clone in playerClones)
            {
                if (clone.IsAlive)
                    clone.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
