using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Parsers
{
    class DBPathParser
    {
        public static string MapNameExtension = ".map";
        public static string MapFolderPath = "Databases/";

        public static string MapNameParser(string mapPath)
        {
            if (mapPath == null || mapPath == "" || String.IsNullOrWhiteSpace(mapPath))
                mapPath = "testMap" + MapNameExtension;

            return (mapPath + MapNameExtension);
        }
    }
}
