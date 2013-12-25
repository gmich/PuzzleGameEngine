namespace PuzzlePrototype.Level.Logic
{
    public class ToggleRed : IBehavior
    {

        //TODO: update logic
        public bool Process(Gate gate)
        {
            if (gate.gateStatus == 0)
                return true;

            return false;
        }
    }
}
