using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Databases.Input
{
    interface IInputDB
    {
        void Load(object entity);

        void Save(object entity);
    }
}
