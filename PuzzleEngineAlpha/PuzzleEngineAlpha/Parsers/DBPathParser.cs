using System;
using System.IO;

namespace PuzzleEngineAlpha.Parsers
{
    class DBPathParser
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string MapNameExtension = ".map";
        public static string MapInfoExtension = ".info";
        public static string MapFolderPath = "Databases/";

        public static string MapNameParser(string mapPath)
        {
            if (mapPath == null || mapPath == "" || String.IsNullOrWhiteSpace(mapPath))
                mapPath = "unNamedInfo" + MapNameExtension;

            return (mapPath + MapNameExtension);
        }

        public static string LevelInfoNameParser(string mapPath)
        {
            if (mapPath == null || mapPath == "" || String.IsNullOrWhiteSpace(mapPath))
                mapPath = "unNamedInfo" + MapInfoExtension;

            return (mapPath + MapInfoExtension);
        }

        public static string[] GetMapNames()
        {
            try
            {
                string[] maps = Directory.GetFiles(MapFolderPath, "*" + MapNameExtension);
                return maps;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        public static string[] GetMapInfoNames()
        {
            try
            {
                string[] mapInfo = Directory.GetFiles(MapFolderPath, "*" + MapInfoExtension);
                return mapInfo;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        public static string GetMapNameFromPath(string path)
        {
            try
            {
                string[] name = path.Split('.');
                return name[0];
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }
    }
}
