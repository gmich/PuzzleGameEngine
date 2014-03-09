namespace PuzzleEngineAlpha.Actions
{
    public class ChangeMiniMapAction : IAction
    {
        Level.MiniMap miniMap;
        int changeStep;

        public ChangeMiniMapAction(Level.MiniMap miniMap,int changeStep)
        {
            this.miniMap = miniMap;
            this.changeStep = changeStep;
        }

        public void Execute()
        {
            miniMap.CurrentMapID += changeStep;
        }
    }
}
