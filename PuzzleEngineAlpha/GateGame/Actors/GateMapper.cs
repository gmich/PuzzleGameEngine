using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GateGame.Actors
{
    class GateMapper
    {
        #region Declarations

        Texture2D black_gate_h;
        Texture2D black_gate_v;
        Texture2D blue_gate_h;
        Texture2D blue_gate_v;
        Texture2D sea_gate_h;
        Texture2D sea_gate_v;

        #endregion

        #region Constructor

        public GateMapper(ContentManager Content)
        {
            black_gate_h = Content.Load<Texture2D>(@"Textures/Gates/black_gate_h");
            black_gate_v = Content.Load<Texture2D>(@"Textures/Gates/black_gate_v");
            blue_gate_h = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_h");
            blue_gate_v = Content.Load<Texture2D>(@"Textures/Gates/blue_gate_v");
            sea_gate_h = Content.Load<Texture2D>(@"Textures/Gates/sea_gate_h");
            sea_gate_v = Content.Load<Texture2D>(@"Textures/Gates/sea_gate_v");
        }

        #endregion

        #region Mapper

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
                    return "sea";
                case 4:
                case 10:
                    return "blue";
                case 5:
                case 11:
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
                default:
                    return null;
            }
        }

        public Rectangle GetCollisionRectangleByID(int id, Vector2 location)
        {
            if (id <=2 || (id>=6 && id<=8))
                return new Rectangle((int)location.X, (int)location.Y + 22, 64, 20);
            else
                return new Rectangle((int)location.X + 22, (int)location.Y, 20, 64);

        }

        public bool IsGateEnabled(int id)
        {
            return (id <= 5);
        }

        public Rectangle GetInteractionRectangleByID(int id, Vector2 location)
        {
            int offSet = 15;

            if (id <= 2 || (id >= 6 && id <= 8))
                return new Rectangle((int)location.X - offSet, (int)location.Y + 22 - offSet, 64 + offSet * 2, 20 + offSet * 2);
            else
                return new Rectangle((int)location.X + 22 - offSet, (int)location.Y - offSet, 20 + offSet * 2, 64 + offSet * 2);
        }

        #endregion

    }
}
