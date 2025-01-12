﻿using CommandSims.Entity;
using CommandSims.Entity.Base;
using CommandSims.Enums;
using CommandSims.Modules.Events;
using CommandSims.Modules.Maps;
using CommandSims.Modules.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Core
{
    public class WorldFrame
    {
        private Random random;
        /// <summary>
        /// 地图
        /// </summary>
        public WorldMap Map { get; set; }

        /// <summary>
        /// 所有NPC
        /// </summary>
        public List<ActiveNpc> NpcList
        {
            get
            {
                return Sims.Context.WorldData.ActiveNpcs;
            }
            set
            {
                Sims.Context.WorldData.ActiveNpcs = value;
            }
        }

        public List<ItemBase> ItemList { get; set; }



        public WorldFrame()
        {
            random = new();
            Map = new WorldMap();
            SetWorldStartTime(Sims.Context.WorldData.WorldTime);
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
            SetWorldStartTime(null);
            // 设置默认天气
            GoRandomWeather();
        }

        /// <summary>
        /// 初始化世界数据
        /// </summary>
        public void InitWorldData()
        {
            // 添加NPC

        }

        #region 天气系统

        /// <summary>
        /// 当前天气
        /// </summary>
        private static string Weather = "晴";

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

        public SimpleListItem GetWorldWeather()
        {
            return WeatherList.First(x => x.Value == Weather);
        }

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
            var randIndex = random.Next(0, WeatherList.Count);
            var nextWeather = WeatherList[randIndex];
            Weather = nextWeather.Value;
            UI.PrintLine(nextWeather.Text);
        }

        #endregion

        #region 时间系统
        /**
         * 地图移动需要时间
         * 玩家操作需要时间
         * 时间精确到分钟
         * 睡觉更新地图NPC事件
         * 
         */

        /// <summary>
        /// 系统时间
        /// </summary>
        private static DateTime WorldTime = DateTime.MinValue;

        public DateTime GetWorldTime() { return WorldTime; }

        public void UpdateWorldTime(int day = 0, int hour = 0, int min = 0)
        {
            var tempYear = WorldTime.Year;
            WorldTime = WorldTime.AddDays(day);
            WorldTime = WorldTime.AddHours(hour);
            WorldTime = WorldTime.AddMinutes(min);
            if (WorldTime.Year - tempYear > 0)
            {
                Sims.Context.Player.Age += WorldTime.Year - tempYear;
            }
        }

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
        public void SetWorldStartTime(DateTime? startTime)
        {
            if (startTime != null)
            {
                WorldTime = startTime.Value;
            }
            else
            {
                WorldTime = DateTime.MinValue;
                WorldTime = WorldTime.AddYears(random.Next(1000, 1100));
                WorldTime = WorldTime.AddDays(random.Next(0, 365));
                WorldTime = WorldTime.AddHours(random.Next(0, 23));
                WorldTime = WorldTime.AddMinutes(random.Next(0, 60));
                WorldTime = WorldTime.AddSeconds(random.Next(0, 60));
            }
        }

        #endregion


        #region NPC

        public void AddNpc()
        {
            // 获取最大ID
            var maxId = Sims.Context.WorldData.ActiveNpcs.OrderByDescending(x => x.Id).First().Id;
            maxId++;

            UI.PrintLine("有新人出生了");
        }
        #endregion
    }
}
