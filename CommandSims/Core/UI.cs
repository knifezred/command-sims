using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Core
{
    public static class UI
    {

        #region Console.Write重写，支持颜色设置，打字机效果

        public static void Print(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Typewriter(string message, int delay = 10)
        {
            var msgs = message.ToArray();
            foreach (var msg in msgs)
            {
                Console.Write($"{msg}");
                Thread.Sleep(delay);
            }

        }

        #endregion

        /// <summary>
        /// 显示角色状态
        /// </summary>
        public static void ShowPlayerStatus(string playerName)
        {

        }
        /// <summary>
        /// 显示角色状态
        /// </summary>
        /// <param name="playerId"></param>
        public static void ShowPlayerStatus(int playerId)
        {
            PrintLine("------------------", ConsoleColor.Green);
            PrintLine("姓名: 张三", ConsoleColor.Cyan);
            PrintLine("相貌: 平平无奇", ConsoleColor.Cyan);
            PrintLine("功力: 深不可测", ConsoleColor.Cyan);
            PrintLine("情绪: 古井无波", ConsoleColor.Cyan);
            PrintLine("------------------", ConsoleColor.Green);

        }
    }
}
