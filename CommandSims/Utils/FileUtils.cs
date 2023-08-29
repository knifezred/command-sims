using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Utils
{

    public class FileUtils
    {
        /// <summary>
        /// 列出目录下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<FileInfo> List(string path, SearchOption searchOption = SearchOption.AllDirectories)
        {
            path = PathUtils.ConvertToAbsolutePath(path);
            if (!Directory.Exists(path))
            {
                return new List<FileInfo>();
            }
            DirectoryInfo folder = new DirectoryInfo(path);
            var fileList = folder.EnumerateFiles("*", searchOption);
            return fileList.ToList();
        }

        public static bool Move(String sourcePath, String targetPath)
        {
            if (File.Exists(targetPath))
            {
                // 防止移动到目标文件夹文件名重复
                targetPath = DuplicateRename(targetPath);
                File.Move(sourcePath, targetPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 文件名重复时自动重命名
        /// 追加 (index),追加后依旧重复的递增index
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>

        public static string DuplicateRename(string path, int index = 0)
        {
            if (File.Exists(path))
            {
                var file = new FileInfo(path);
                path = file.FullName.Replace(" (" + (index - 1) + ").", "").Replace("." + file.Extension, " (" + index + ")." + file.Extension);
                if (File.Exists(path))
                {
                    path = DuplicateRename(path, index++);
                }
            }
            return path;
        }


        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string GetFileSize(string path)
        {
            if (File.Exists(PathUtils.ConvertToAbsolutePath(path)))
            {
                FileInfo fileInfo = new FileInfo(PathUtils.ConvertToAbsolutePath(path));
                return GetFileSize(fileInfo.Length);
            }
            else
            {
                return "0KB";
            }
        }

        private static string GetFileSize(long size)
        {
            const double num = 1024.00; //byte
            if (size < num)
                return size + "B";
            if (size < Math.Pow(num, 2))
                return (size / num).ToString("f2") + "KB"; //kb
            if (size < Math.Pow(num, 3))
                return (size / Math.Pow(num, 2)).ToString("f2") + "MB"; //M
            if (size < Math.Pow(num, 4))
                return (size / Math.Pow(num, 3)).ToString("f2") + "GB"; //G

            return (size / Math.Pow(num, 4)).ToString("f2") + "TB"; //T
        }


        /// <summary>
        /// 读取本地文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            using (FileStream fsRead = new FileStream(PathUtils.ConvertToAbsolutePath(path), FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string text = Encoding.UTF8.GetString(heByte);
                return text;
            }
        }

        /// <summary>
        /// 写入本地文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteFile(string content, string path)
        {
            path = CheckFolder(path);
            //写入文件
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize: 4096))
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                fs.Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 写入本地文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>

        public static void WriteAllLines(string path, List<string> contents)
        {
            path = CheckFolder(path);
            //写入文件
            File.WriteAllLines(path, contents, Encoding.UTF8);
        }


        private static string CheckFolder(string path)
        {
            path = PathUtils.ConvertToAbsolutePath(path);
            //判断文件夹是否存在，不存在则生成文件夹
            string floder = path.Substring(0, path.LastIndexOf("\\"));
            if (!Directory.Exists(floder))
            {
                Directory.CreateDirectory(floder);
            }
            return path;
        }



        /// <summary>
        /// 比较文件是否相同(哈希校验)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="secondFilePath"></param>
        /// <returns></returns>
        public static bool CompareFile(string filePath, string secondFilePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            //计算第一个文件的哈希值
            var hash = HashAlgorithm.Create();
            var stream_1 = new FileStream(filePath, FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();
            //计算第二个文件的哈希值
            var stream_2 = new FileStream(secondFilePath, FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();
            //比较两个哈希值
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                return true;
            else
                return false;
        }

    }
}
