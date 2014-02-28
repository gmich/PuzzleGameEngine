using System.IO;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    public interface IMapDB
    {
        MapSquare[,] Load(string path);

        void Save(string path, MapSquare[,] mapCells);
    }
}
