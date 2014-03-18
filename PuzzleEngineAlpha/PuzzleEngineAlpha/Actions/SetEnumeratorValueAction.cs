using System;

namespace PuzzleEngineAlpha.Actions
{
    using Utils;
    using Components.Areas;

    public class SetEnumeratorValueAction : IAction
    {
        ComponentEnumerator enumerator;
        int value;

        public SetEnumeratorValueAction(ComponentEnumerator enumerator,int value)
        {
            this.enumerator = enumerator;
            this.value = value;
        }

        public void Execute()
        {
            this.enumerator.SetCurrentValue(value);
        }
    }
}
