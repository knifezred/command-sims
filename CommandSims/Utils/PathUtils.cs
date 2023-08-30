using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Utils
{
    public class PathUtils
    {

        /// <summary>
        /// 相对路径转绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertToAbsolutePath(string path)
        {
            //绝对路径
            if (path.Contains(":\\"))
            {
                return path;
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetRelativeAppDomainPath(string path)
        {
            if (path.StartsWith(@"/"))
            {
                return path;
            }
            return path.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Replace(@"\", @"/");
        }

    }
}
