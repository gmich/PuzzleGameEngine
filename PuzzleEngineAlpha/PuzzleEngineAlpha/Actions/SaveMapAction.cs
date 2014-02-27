using System.IO;

namespace PuzzleEngineAlpha.Actions
{
    class SaveMapAction : IAction
    {

        Level.MapHandler mapHandler;
        Components.TextBoxes.TextBox textBox;

        public SaveMapAction(Level.MapHandler mapHandler, Components.TextBoxes.TextBox textBox)
        {
            this.mapHandler = mapHandler;
            this.textBox = textBox;
        }

        public void Execute()
        {
            string path = Parsers.InputParser.MapPathParser(textBox.Text);
            mapHandler.SaveMapAsynchronously(new FileStream(path, FileMode.Create));
        }
    }
}
