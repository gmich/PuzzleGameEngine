using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace EngineTester
{
    using PuzzleEngineAlpha.Components;
    using PuzzleEngineAlpha.Resolution;
    using PuzzleEngineAlpha.Components.Buttons;

    [TestClass]
    public class GUIElementTest
    {
        [TestMethod]
        public void GUIElementIntersection()
        {
            AGUIComponent component = new MenuButton(new DrawProperties(), new DrawProperties(), new DrawProperties(), new DrawTextProperties(), new Vector2(1, 1), new Vector2(3, 3),ResolutionHandler.ScreenRectangle);
            bool intersectionResult = component.Intersects(new Vector2(2, 2));
            
            Assert.IsTrue(intersectionResult);
        }
    }
}
