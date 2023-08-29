using CommandSims.Utils;
using System;
using System.IO;

namespace KnifeZ.Unity.Helpers
{
    public class LogHelper
    {
        public static void LogError(Exception exception, string path = "logs/log.txt")
        {
            path = PathUtils.ConvertToAbsolutePath(path);
            FileInfo fi = new FileInfo(path);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create();
            }

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + "[Error] -- " + exception.Message);
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + "[Error] -- " + exception.StackTrace);
            }
        }

        public static void LogInfo(string message, string path = "logs/log.txt")
        {
            Log(message, "Info", path);
        }

        private static void Log(string info, string logLevel = "Debug", string path = "logs/log.txt")
        {
            path = PathUtils.ConvertToAbsolutePath(path);
            FileInfo fi = new FileInfo(path);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create();
            }

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss][") + logLevel + "] -- " + info);
            }
        }

    }
}
