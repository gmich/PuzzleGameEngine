using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleEngineAlpha.Parsers
{
    class InputParser
    {
        static string MapNameExtension = ".map";

        public static string MapPathParser(string mapPath)
        {
            string correctPath = mapPath;
            if (correctPath == null || correctPath == "" || String.IsNullOrWhiteSpace(correctPath))
                correctPath = "testMap" + MapNameExtension;

            correctPath += MapNameExtension;

            return correctPath;
        }
    }
}
