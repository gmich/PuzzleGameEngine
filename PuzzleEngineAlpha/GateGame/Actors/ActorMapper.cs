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
        Texture2D sea_gate_h;
        Texture2D sea_gate_v;
        Texture2D button_sea;
        Texture2D button_blue;
        Texture2D button_black;
        PuzzleEngineAlpha.Level.TileMap tileMap;

        #endregion

        #region Constructor

        public ActorMapper(ContentManager Content, PuzzleEngineAlpha.Level.TileMap tileMap)
        {
            black_gate_h = Content.Load<Texture2D>(@"Textures/Gates/black_gate_h");
            black_gate_v = Content.Load<Texture2D>(@"Textures/Gates/black_gate_v");
            blue_gate_h = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_h");
            blue_gate_v = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_v");
            sea_gate_h = Content.Load<Texture2D>(@"Textures/Gates/sea_gate_h");
            sea_gate_v = Content.Load<Texture2D>(@"Textures/Gates/sea_gate_v");
            button_sea = Content.Load<Texture2D>(@"Textures/Buttons/button_sea");
            button_blue = Content.Load<Texture2D>(@"Textures/Buttons/button_blue");
            button_black = Content.Load<Texture2D>(@"Textures/Buttons/button_black");
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

        public string GetTagByID(int id)
        {
            switch(id)
            {
                case 0:
                case 6:
                    return "sea";
                case 1:
                case 7:
                    return "blue";
                case 2:
                case 8:
                    return "black";
                case 3:
                case 9:
                case 12:
                    return "sea";
                case 4:
                case 10:
                case 13:
                    return "blue";
                case 5:
                case 11:
                case 14:
                    return "black";
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
                    return sea_gate_h;
                case 1:
                case 7:
                    return blue_gate_h;
                case 2:
                case 8:
                    return black_gate_h;
                case 3:
                case 9:
                    return sea_gate_v;
                case 4:
                case 10:
                    return blue_gate_v;
                case 5:
                case 11:
                    return black_gate_v;
                case 12:
                    return button_sea;
                case 13:
                    return button_blue;
                case 14:
                    return button_black;
                default:
                    return null;
            }
        }

        public Rectangle GetCollisionRectangleByID(int id, Vector2 location)
        {
            if (id <=2 || (id>=6 && id<=8))
                return new Rectangle((int)location.X, (int)location.Y + tileMap.TileHeight/3+2, tileMap.TileWidth,tileMap.TileHeight/3-2);
            else if(id<=11)
                return new Rectangle((int)location.X + tileMap.TileWidth/3+2, (int)location.Y, tileMap.TileWidth/3-2,  tileMap.TileHeight);
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
            else
                return new Rectangle((int)location.X, (int)location.Y, tileMap.TileWidth, tileMap.TileHeight);
        }

        #endregion

    }
}
