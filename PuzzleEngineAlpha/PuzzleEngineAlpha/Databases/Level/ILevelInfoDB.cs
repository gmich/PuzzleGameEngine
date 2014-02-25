using System.IO;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    interface ILevelInfoDB
    {
        LevelInfo Load(FileStream fileStream);

        void Save(FileStream fileStream, LevelInfo levelInfo);
    }
}
