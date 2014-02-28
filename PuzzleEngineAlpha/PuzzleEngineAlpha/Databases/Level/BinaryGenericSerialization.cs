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

        public T Load(string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                T t = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
                return t;
            }
            catch (Exception ex)
            {
                log.Error("Loading " + path + "failed due to " + ex.Message);
                return default(T);
            }
        }

        public void Save(string path, T t)
        {
            try
            {
                string folder = Parsers.DBPathParser.MapFolderPath;
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);
                FileStream fileStream = new FileStream(folder + path, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, t);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("Saving " + path + "failed due to " + ex.Message);
            }
        }

    }
}