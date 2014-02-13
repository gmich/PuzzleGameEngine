using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Actions
{
    using GUI.ActionReceivers;

    class AppendConsoleText : IAction
    {

        string text;

        public AppendConsoleText(string text)
        {
            this.text = text;
        }

        public void Execute()
        {
            Console.WriteLine(text);
        }
    }
}
