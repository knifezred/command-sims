using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Service
{
    public class ArchiveService
    {
        public List<string> ListArchives()
        {
            var path = Path.Join(Environment.CurrentDirectory, "App_Data", "Saves");
            DirectoryInfo directoryInfo = new(path);
            var files = directoryInfo.GetFiles();
            return new List<string>();
        }
    }
}
