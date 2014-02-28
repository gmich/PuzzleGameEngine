using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    public class BinaryLevelInfoSerialization : ILevelInfoDB
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LevelInfo Load(string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                LevelInfo levelInfo = (LevelInfo)formatter.Deserialize(fileStream);
                fileStream.Close();
                return levelInfo;
            }
            catch (Exception ex)
            {
                log.Error("Loading " + path + "failed due to " + ex.Message);
                return null;
            }
        }

        public void Save(string path, LevelInfo levelInfo)
        {
            try
            {
                string folder = Parsers.DBPathParser.MapFolderPath;
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);
                FileStream fileStream = new FileStream(folder + path, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, levelInfo);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving " + path + "failed due to " + ex.Message);
                throw ex;
            }
        }

    }
}
