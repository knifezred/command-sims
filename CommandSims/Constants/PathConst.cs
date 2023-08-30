﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Constants
{
    public class PathConst
    {
        /// <summary>
        /// 存档路径
        /// </summary>
        public static string ARCHIVE_PATH = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CommandSims", "Saves");


    }
}