using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PuzzleEngineAlpha.Databases.Level
{
    using PuzzleEngineAlpha.Level;

    public class BinaryGenericSerialization<T>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public T Load(FileStream fileStream)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                T t = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
                return t;
            }
            catch (Exception ex)
            {
                log.Error("Loading " + fileStream + "failed due to " + ex.Message);
                return default(T);
            }
        }

        public void Save(FileStream fileStream, T t)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, t);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving " + fileStream + "failed due to " + ex.Message);
            }
        }

    }
}