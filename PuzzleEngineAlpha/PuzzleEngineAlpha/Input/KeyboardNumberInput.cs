using Microsoft.Xna.Framework.Input;

namespace PuzzleEngineAlpha.Input
{
    public class KeyboardNumberInput : KeyboardInput
    {
        public override string Convert(Keys key)
        {
            string output = "";

            if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                output += ((int)(key - Keys.NumPad0)).ToString();
            else if (key >= Keys.D0 && key <= Keys.D9)
                output += ((int)(key - Keys.D0)).ToString();
            else if (key == Keys.Back)
                output += "\b";

            return output;
        }

    }
}