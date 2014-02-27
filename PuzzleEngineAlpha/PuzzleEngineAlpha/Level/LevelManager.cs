using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PuzzleEngineAlpha.Level
{
    using Actors;

    public class LevelManager
    {
        List<IActor> actors;
       
        public LevelManager()
        {
            actors = new List<IActor>();
        }
    }
}
