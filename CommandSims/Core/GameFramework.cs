using CommandSims.Constants;
using CommandSims.Data;
using CommandSims.Entity.Archive;
using CommandSims.Stories;
using CommandSims.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CommandSims.Core
{
    public class GameFramework
    {
        #region 存档
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="saveName"></param>
        public void SaveArchive(string saveName)
        {
            if (!saveName.Any())
            {
                saveName = "AutoSaved";
            }
            var archivePath = Path.Join(PathConst.ARCHIVE_PATH, saveName);
            var data = JsonSerializer.Serialize(Sims.PlayerData);
            FileUtils.WriteFile(data, archivePath);
        }

        /// <summary>
        /// 读档
        /// </summary>
        /// <param name="archiveName"></param>
        public void LoadArchive(string archiveName)
        {
            UI.PrintLine("存档读取中...");
            var archives = FileUtils.List(PathConst.ARCHIVE_PATH);
            // todo 读取存档文件
            FileInfo? archive;
            if (archiveName == "")
            {
                archive = archives.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
            }
            else
            {
                archive = archives.AsQueryable().Where(x => x.Name == archiveName).FirstOrDefault();
            }
            if (archive != null)
            {
                var archiveDataText = FileUtils.ReadFile(archive.FullName);
                var archiveData = JsonSerializer.Deserialize<ArchiveData>(archiveDataText);
                if (archiveData != null)
                {
                    Sims.PlayerData = archiveData;
                    UI.PrintLine("存档加载成功");
                    UI.ShowPalyerInfo();
                }
                else
                {
                    UI.PrintLine("存档读取失败，请重新开档或读取其他存档");
                }
            }
            else
            {
                var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[[" + archiveName + "]]不存在，是否创建新存档？")
                    .PageSize(10)
                    .AddChoices(new string[]
                    {
                        "1. 确定","2. 读取其他存档"
                    }));
                if (result.StartsWith("1"))
                {
                    Console.WriteLine();
                    S0_SomeoneBorned someoneBorned = new();
                    someoneBorned.PlayerBorn();
                }
                else if (result.StartsWith("2"))
                {
                    UI.PrintLine("请输入存档名称");
                    var newArchive = Console.ReadLine();
                    if (newArchive != null)
                    {
                        LoadArchive(newArchive);
                    }
                }
                else
                {
                    UI.PrintLine("无法识别的输入选项，将退回");
                }
            }

        }

        #endregion
    }
}
