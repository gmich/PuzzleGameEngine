using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Level.Editor
{
    public class TileManager
    {

        static public Rectangle ActorSourceRectangle
        {
            get;
            set;
        }

        static public Rectangle TileSourceRectangle
        {
            get;
            set;
        }

        static public Texture2D TileSheet
        {
            get;
            set;
        }

        static public Texture2D ActorTileSheet
        {
            get;
            set;
        }

        static public bool ShowActors
        {
            get;
            set;
        }

        static public bool ShowPassable
        {
            get;
            set;
        }

        static MapSquare mapSquare;

        static public MapSquare MapSquare
        {
            get
            {
                return mapSquare;
            }
            set
            {
                mapSquare = value;

                if (value != null)
                {
                    if (ShowActors)
                        Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 80), "selected ID: " + mapSquare.ActorID);
                    else
                        Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 80), "selected ID: " + mapSquare.LayerTile);

                    Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 105), "passable: " + mapSquare.Passable);
                    Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 135), "code value: " + mapSquare.CodeValue);
                    Scene.Editor.DiagnosticsScene.LargestWidth = 190;
                }
                else
                {
                    Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 80), " ");
                    Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 105), " ");
                    Scene.Editor.DiagnosticsScene.SetText(new Vector2(5, 135), " ");
                    Scene.Editor.DiagnosticsScene.LargestHeight = 75;
                }
            }
        }
    }
}
