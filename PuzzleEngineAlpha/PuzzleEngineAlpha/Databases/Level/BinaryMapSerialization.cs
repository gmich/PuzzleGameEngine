using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    public class BinaryMapSerialization : IMapDB
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MapSquare[,] Load(string path)
        {
            try
            {
                string editedName = Parsers.DBPathParser.MapNameParser(path);
                FileStream fileStream = new FileStream(editedName, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();
                MapSquare[,] mapCells = (MapSquare[,])formatter.Deserialize(fileStream);
                fileStream.Close();

                return mapCells;
            }
            catch (Exception ex)
            {
                log.Error("Loading Map " + path + "failed due to " + ex.Message);
                throw ex;
            }
        }

        public void Save(string path, MapSquare[,] mapCells)
        {
            try
            {
                string folder = Parsers.DBPathParser.MapFolderPath;
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);

                string editedName = Parsers.DBPathParser.MapNameParser(path);
                FileStream fileStream = new FileStream(folder + editedName, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, mapCells);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving Map " + path + "failed due to " + ex.Message);
                throw ex;
            }
        }
    }
}
