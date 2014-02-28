namespace PuzzleEngineAlpha.Actions
{
    class SaveMapAction : IAction
    {
        Scene.Editor.MapHandlerScene mapHandler;
        Components.TextBoxes.TextBox textBox;

        public SaveMapAction(Scene.Editor.MapHandlerScene mapHandler, Components.TextBoxes.TextBox textBox)
        {
            this.mapHandler = mapHandler;
            this.textBox = textBox;
        }

        public void Execute()
        {
            string path = Parsers.DBPathParser.MapNameParser(textBox.Text);
            mapHandler.SaveMapAsynchronously(path);
        }
    }
}
