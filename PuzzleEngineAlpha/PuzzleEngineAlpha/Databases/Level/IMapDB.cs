using System.IO;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    interface IMapDB
    {
        MapSquare[,] Load(FileStream fileStream);

        void Save(FileStream fileStream, MapSquare[,] mapCells);
    }
}
