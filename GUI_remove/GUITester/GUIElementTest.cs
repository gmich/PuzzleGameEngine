using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace GUITester
{
    using GUI.Components;
    using GUI.Components.Buttons;

    [TestClass]
    public class GUIElementTest
    {
        [TestMethod]
        public void GUIElementIntersection()
        {
            AGUIElement element = new MenuButton(new DrawProperties(), new DrawProperties(), new DrawProperties(), new DrawTextProperties(), new Vector2(1, 1), new Vector2(3, 3));
            bool intersectionResult = element.Intersects(new Vector2(2, 2));
            
            Assert.IsTrue(intersectionResult);
        }
    }
}
