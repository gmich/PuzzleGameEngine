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

         public LevelInfo Load(FileStream fileStream)
        {
            try
            {                
                BinaryFormatter formatter = new BinaryFormatter();
                LevelInfo levelInfo = (LevelInfo)formatter.Deserialize(fileStream);
                fileStream.Close();
                return levelInfo;
            }
            catch (Exception ex)
            {
                log.Error("Loading " + fileStream + "failed due to " + ex.Message);
                return null;
            }
        }

         public void Save(FileStream fileStream, LevelInfo levelInfo)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, levelInfo);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving " + fileStream + "failed due to " + ex.Message);
            }
        }

    }
}
