namespace PuzzleEngineAlpha.Actions
{
    using Scene;

    public class ToggleActiveSceneryAction : IAction
    {
        SceneDirector sceneDirector;

        public ToggleActiveSceneryAction(SceneDirector sceneDirector)
        {
            this.sceneDirector = sceneDirector;
        }

        public void Execute()
        {
            sceneDirector.SwapActiveScenes();
        }
    }
}
