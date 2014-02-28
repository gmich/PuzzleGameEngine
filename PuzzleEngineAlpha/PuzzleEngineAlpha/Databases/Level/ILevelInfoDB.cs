using System.IO;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    public interface ILevelInfoDB
    {
        LevelInfo Load(string path);

        void Save(string path, LevelInfo levelInfo);
    }
}
