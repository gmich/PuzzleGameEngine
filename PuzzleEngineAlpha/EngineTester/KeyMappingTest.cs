using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;

namespace EngineTester
{
    using PuzzleEngineAlpha.Input;
    [TestClass]
    public class KeyMappingTest
    {

        [TestMethod]
        public void TestInputUtilitiesGetKeyWithCorrectArgument()
        {
            //initialize mapping in constructor
            InputUtilities inputUtilities = new InputUtilities();

            Keys keyToTest = InputUtilities.GetKey("ArrowUp");
            Keys expectedKey = Keys.Up;

            Assert.AreEqual(expectedKey, keyToTest);
        }
    }
}
