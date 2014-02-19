using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace GUI.Components
{

    public struct DrawProperties
    {
        public Texture2D texture;
        public float layer;
        public float transparency;
        public float rotation;
        public Color color;

        public DrawProperties(Texture2D texture, float layer, float transparency, float rotation, Color color)
        {
            this.texture = texture;
            this.layer = layer;
            this.transparency = transparency;
            this.rotation = rotation;
            this.color = color;
        }
    }

    public struct DrawTextProperties
    {
        public string text;
        public int size;
        public SpriteFont font;
        public Color textColor;
        public float textLayer;
        public float textScale;

        public DrawTextProperties(string text, int size, SpriteFont font, Color textColor, float textLayer, float textScale)
        {
            this.text = text;
            this.size = size;
            this.font = font;
            this.textColor = textColor;
            this.textLayer = 0.7f;
            this.textScale = 1.0f;
        }
    }

}
