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

         public MapSquare[,] Load(FileStream fileStream)
        {
            try
            {                
                BinaryFormatter formatter = new BinaryFormatter();
                MapSquare[,] mapCells = (MapSquare[,])formatter.Deserialize(fileStream);
                fileStream.Close();
                return mapCells;
            }
            catch (Exception ex)
            {
                log.Error("Loading Map " + fileStream + "failed due to " + ex.Message);
                return null;
            }
        }

        public void Save(FileStream fileStream,MapSquare[,] mapCells)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, mapCells);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving Map " + fileStream + "failed due to " + ex.Message);
            }
        }

    }
}
