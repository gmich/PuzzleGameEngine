using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GateGame.Actors
{
    class ActorMapper
    {
        #region Declarations

        Texture2D black_gate_h;
        Texture2D black_gate_v;
        Texture2D blue_gate_h;
        Texture2D blue_gate_v;
        Texture2D yellow_gate_h;
        Texture2D yellow_gate_v;
        Texture2D button_yellow;
        Texture2D button_blue;
        Texture2D button_black;
        Texture2D coin;
        Texture2D hidden_wall;
        Texture2D clone_box;

        PuzzleEngineAlpha.Level.TileMap tileMap;

        #endregion

        #region Constructor

        public ActorMapper(ContentManager Content, PuzzleEngineAlpha.Level.TileMap tileMap)
        {
            black_gate_h = Content.Load<Texture2D>(@"Textures/Gates/black_gate_h");
            black_gate_v = Content.Load<Texture2D>(@"Textures/Gates/black_gate_v");
            blue_gate_h = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_h");
            blue_gate_v = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_v");
            yellow_gate_h = Content.Load<Texture2D>(@"Textures/Gates/yellow_gate_h");
            yellow_gate_v = Content.Load<Texture2D>(@"Textures/Gates/yellow_gate_v");
            button_yellow = Content.Load<Texture2D>(@"Textures/Buttons/button_yellow");
            button_blue = Content.Load<Texture2D>(@"Textures/Buttons/button_blue");
            button_black = Content.Load<Texture2D>(@"Textures/Buttons/button_black");
            coin = Content.Load<Texture2D>(@"Textures/Items/coin");
            hidden_wall = Content.Load<Texture2D>(@"Textures/Items/hidden_wall");
            clone_box = Content.Load<Texture2D>(@"Textures/Items/clone_box");
            this.tileMap = tileMap;
        }

        #endregion

        #region Mapper

        public int LastGateID
        {
            get
            {
                return 11;
            }
        }
        public int CoinID
        {
            get
            {
                return 15;
            }
        }

        public int HiddenWallID
        {
            get
            {
                return 16;
            }
        }
        public int CloneBoxID
        {
            get
            {
                return 17;
            }
        }

        public string GetTagByID(int id)
        {
            switch(id)
            {
                case 0:
                case 6:
                    return "yellow";
                case 1:
                case 7:
                    return "blue";
                case 2:
                case 8:
                    return "black";
                case 3:
                case 9:
                case 12:
                    return "yellow";
                case 4:
                case 10:
                case 13:
                    return "blue";
                case 5:
                case 11:
                case 14:
                    return "black";
                case 15:
                    return "coin";
                case 16:
                    return "hiddenWall";
                case 17:
                    return "cloneBox";
                default:
                    return "tagNotFound";
            }
        }

        public Texture2D GetTextureByID(int id)
        {
            switch (id)
            {
                case 0:
                case 6:
                    return yellow_gate_h;
                case 1:
                case 7:
                    return blue_gate_h;
                case 2:
                case 8:
                    return black_gate_h;
                case 3:
                case 9:
                    return yellow_gate_v;
                case 4:
                case 10:
                    return blue_gate_v;
                case 5:
                case 11:
                    return black_gate_v;
                case 12:
                    return button_yellow;
                case 13:
                    return button_blue;
                case 14:
                    return button_black;
                case 15:
                    return coin;
                case 16:
                    return hidden_wall;
                case 17:
                    return clone_box;
                default:
                    return null;
            }
        }

        public Rectangle GetCollisionRectangleByID(int id, Vector2 location)
        {
            if (id <= 2 || (id >= 6 && id <= 8))
                return new Rectangle((int)location.X, (int)location.Y + tileMap.TileHeight / 3 + 2, tileMap.TileWidth, tileMap.TileHeight / 3 - 2);
            else if (id <= 11)
                return new Rectangle((int)location.X + tileMap.TileWidth / 3 + 2, (int)location.Y, tileMap.TileWidth / 3 - 2, tileMap.TileHeight);
            else if (id > 11 && id < 15)
                return new Rectangle((int)location.X, (int)location.Y, tileMap.TileWidth, tileMap.TileHeight);
            else if (id == 15) 
                return new Rectangle((int)location.X + 19, (int)location.Y + 20, 24, 23);
            else
                return new Rectangle((int)location.X, (int)location.Y, tileMap.TileWidth, tileMap.TileHeight);

        }

        public bool IsGateEnabled(int id)
        {
            return (id <= 5);
        }

        public Rectangle GetInteractionRectangleByID(int id, Vector2 location)
        {
            int offSet = 15;

            if (id <= 2 || (id >= 6 && id <= 8))
                return new Rectangle((int)location.X - offSet, (int)location.Y + tileMap.TileHeight / 3 - offSet, tileMap.TileWidth + offSet * 2, tileMap.TileHeight / 3 - 2 + offSet * 2);
            else if (id <= 11)
                return new Rectangle((int)location.X + tileMap.TileWidth / 3 - offSet, (int)location.Y - offSet, tileMap.TileWidth / 3 - 2 + offSet * 2, tileMap.TileHeight + offSet * 2);
            else if (id > 11 && id < 15)
                return new Rectangle((int)location.X, (int)location.Y, tileMap.TileWidth, tileMap.TileHeight);
            else if (id == 15)
                return new Rectangle((int)location.X + 19, (int)location.Y + 20, 24, 23);
            else
                return new Rectangle((int)location.X, (int)location.Y, tileMap.TileWidth, tileMap.TileHeight);

        }

        #endregion

    }
}
