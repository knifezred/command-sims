using KnifeZ.Unity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Core
{
    public class WorldFramework
    {
        private Random random;

        public WorldFramework()
        {
            random = new();
        }

        /// <summary>
        /// 创建新世界
        /// </summary>
        /// <param name="seed"></param>
        public void CreateNewWorld(int seed)
        {
            if (seed > 0)
            {
                random = new(seed);
            }
            else
            {
                random = new();
            }
            // 设置初始时间
            SetWorldStartTime();
            // 设置默认天气
            GoRandomWeather();
        }

        /// <summary>
        /// 初始化世界数据
        /// </summary>
        public void InitWorldData()
        {

        }

        #region 天气系统

        /// <summary>
        /// 当前天气
        /// </summary>
        public static string Weather = "晴";

        //"阴","大雾","雷阵雨","小雨","中雨","大雨","暴雨","小雪","中雪","大雪","暴雪",
        public List<SimpleListItem> WeatherList = new()
        {
            new SimpleListItem()
            {
                Text="天气转晴了",
                Value="晴"
            },
            new SimpleListItem()
            {
                Text="天空飘来几朵云",
                Value="多云"
            },
            new SimpleListItem()
            {
                Text="一片乌云飘来，下起了小雨",
                Value="小雨"
            },
        };

        public void ChangeWeather(string weather)
        {
            // 天气是否存在
            if (WeatherList.Any(x => x.Value.Equals(weather)))
            {
                Weather = weather;
            }
        }

        public void GoRandomWeather()
        {
            Random random = new();
            var randIndex = random.Next(0, WeatherList.Count);
            var nextWeather = WeatherList[randIndex];
            Weather = nextWeather.Value.ToString();
            UI.PrintLine(nextWeather.Text.ToString());


        }

        #endregion

        #region 时间系统
        /// <summary>
        /// 系统时间
        /// </summary>
        public static DateTime WorldTime = DateTime.MinValue;

        /// <summary>
        /// 生成NPC出生时间
        /// </summary>
        /// <returns></returns>
        public DateTime GenerateBronTime()
        {
            var bornTime = WorldTime;
            bornTime = bornTime.AddDays(random.Next(0, 365));
            bornTime = bornTime.AddYears(random.Next(-100, 1));
            return bornTime;
        }

        /// <summary>
        /// 设置初始时间(玩家出生时间)
        /// </summary>
        public void SetWorldStartTime()
        {
            WorldTime = DateTime.MinValue;
            WorldTime = WorldTime.AddYears(random.Next(1000, 1100));
            WorldTime = WorldTime.AddDays(random.Next(0, 365));
            WorldTime = WorldTime.AddHours(random.Next(0, 23));
            WorldTime = WorldTime.AddMinutes(random.Next(0, 60));
            WorldTime = WorldTime.AddSeconds(random.Next(0, 60));
        }

        #endregion

    }
}
