using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level
{
    using Camera;
    using Input;

    public class EditorTileMap : TileMap
    {
        #region Declarations

        Texture2D frameTexture;

        #endregion

        #region Constructor

        public EditorTileMap(Vector2 cameraPosition, ContentManager Content, int tileWidth, int tileHeight,bool showGrid)
            : base(cameraPosition, Content, tileWidth, tileHeight)
        {
            this.frameTexture = Content.Load<Texture2D>(@"Buttons/tileFrame");
            this.ShowGrid = showGrid;
        }

        #endregion

        #region Properties

        public bool ShowGrid
        {
            get;
            set;
        }

        public override Color GetColor(Rectangle rectangle)
        {

            if (InputHandler.MouseRectangle.Intersects(rectangle))
            {

                if (InputHandler.LeftButtonIsClicked())
                    return new Color(200, 200, 200);
                else
                    return new Color(220, 220, 220);
            }
            else
                return Color.White;
        }

        #endregion

        #region Draw

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ShowGrid)
            {

                for (int x = StartX; x <= EndX; x++)
                    for (int y = StartY; y <= EndY; y++)
                    {
                        if ((x >= 0) && (y >= 0) &&
                            (x < MapWidth) && (y < MapHeight))
                        {
                            spriteBatch.Draw(frameTexture, CellScreenRectangle(x, y), null,
                                   Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                        }
                    }
            }
            base.Draw(spriteBatch);
        }

        #endregion

    }
}
