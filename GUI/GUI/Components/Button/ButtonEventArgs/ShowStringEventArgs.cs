using System;
using Microsoft.Xna.Framework;

namespace Button.ButtonEventArgs
{
    class ShowStringEventArgs : EventArgs
    {
        public readonly string text;

        public ShowStringEventArgs(string text)
        {
            this.text = text;
        }
    }
}
