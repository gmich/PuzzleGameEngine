using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace PuzzlePrototypeTester
{
    using PuzzlePrototype.Camera;

    [TestClass]
    public class CameraTest
    {
        [TestMethod]
        public void TestingCameraMovement()
        {
            Camera camera = new Camera(new Vector2(0,0),new Vector2(100,100));
            camera.Move(new Vector2(10, 10));

            Vector2 expectedLocation = new Vector2(10, 10);

            Assert.AreEqual(expectedLocation, camera.Position);
        }
    }
}
