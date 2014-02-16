using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleEngineAlpha.Diagnostics
{
    static class WindowText
    {
        static Dictionary<Vector2, String> texts;
        static SpriteFont font;

        public static void Initialize(SpriteFont font)
        {
            WindowText.font = font;
            texts = new Dictionary<Vector2, string>();
        }
        public static void AddText(Vector2 location, string text)
        {
            WindowText.texts.Add(location, text);
        }

        public static void SetText(Vector2 location, string text)
        {
            WindowText.texts[location] =  text;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Vector2, string> item in texts)
            {
                spriteBatch.DrawString(font,item.Value,item.Key,Color.Black);
            }
        }

    }
}
