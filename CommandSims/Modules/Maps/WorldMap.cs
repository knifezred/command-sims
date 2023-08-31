using CommandSims.Core;
using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Maps
{
    public class WorldMap
    {
        private List<MapEntity> MapList;

        public WorldMap()
        {
            MapList = new List<MapEntity>();
            InitMapData();

        }

        public void InitMapData()
        {
            MapList.Clear();
            #region BlackHouse
            MapList.Add(new MapEntity()
            {
                LocationX = 0,
                LocationY = 0,
                Id = 1,
                ParentId = 0,
                CanOut = false,
                Name = "小黑屋",
                Description = "一间残破的小木屋",
                Type = MapType.Building
            });
            MapList.Add(new MapEntity()
            {
                LocationX = 0,
                LocationY = 0,
                ParentId = 1,
                CanOut = true,
                Name = "客厅1",
                Description = "这是一间客厅1",
                Type = MapType.Room,
                Id = 6,
            });
            MapList.Add(new MapEntity()
            {
                LocationX = 1,
                LocationY = 1,
                ParentId = 1,
                CanOut = false,
                Name = "房间1",
                Description = "这是一间客厅3",
                Type = MapType.Room,
                Id = 3,
            });
            MapList.AddRange(new List<MapEntity>() {new MapEntity()
            {
                LocationX = 0,
                LocationY = 1,
                ParentId = 1,
                CanOut = false,
                Name = "客厅2",
                Description = "这是一间客厅2",
                Type = MapType.Room,
                Id = 2,
            }, new MapEntity()
            {
                LocationX = 2,
                LocationY = 1,
                ParentId = 1,
                CanOut = false,
                Name = "房间2",
                Description = "这是一间客厅4",
                Type = MapType.Room,
                Id = 4,
            }, new MapEntity()
            {
                LocationX = 2,
                LocationY = 0,
                ParentId = 1,
                CanOut = false,
                Name = "房间3",
                Description = "这是一间客厅5",
                Type = MapType.Room,
                Id = 5,
            }            });
            #endregion

        }

        /// <summary>
        /// 根据id获取地图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MapEntity GetMapById(int id)
        {
            if (id == 0)
            {
                if (Sims.Context.CurrentMap != null && Sims.Context.CurrentMap.Id > 0)
                {
                    id = Sims.Context.CurrentMap.Id;
                }
            }
            var map = MapList.FirstOrDefault(x => x.Id == id);
            if (map == null)
            {
                return Sims.World.Map.MapList.First();
            }
            return map;
        }

        /// <summary>
        /// 是否为可进入房间
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public bool CanEnter(int mapId)
        {
            return MapList.Any(x => x.ParentId == mapId);
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public MapEntity GoMapEnterRoom(int mapId)
        {
            var rooms = MapList.Where(x => x.ParentId == mapId && x.CanOut == true).ToList();
            if (rooms.Any())
            {
                return rooms[0];
            }
            else
            {
                UI.PrintLine("没有什么值得探索的", ConsoleColor.DarkGray);
                return GetMapById(mapId);
            }

        }

        /// <summary>
        /// 获取相连地图列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MapEntity> GetArroundMaps(int id)
        {
            var centerMap = GetMapById(id);
            var list = new List<MapEntity>
            {
                centerMap
            };
            var neerMaps = MapList.Where(x => x.ParentId == centerMap.ParentId).ToList();
            var top = neerMaps.FirstOrDefault(x => x.LocationX == centerMap.LocationX && x.LocationY == (centerMap.LocationY + 1));
            if (top != null)
            {
                list.Add(top);
            }
            var bottom = neerMaps.FirstOrDefault(x => x.LocationX == centerMap.LocationX && x.LocationY == (centerMap.LocationY - 1));
            if (bottom != null)
            {
                list.Add(bottom);
            }
            var left = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX - 1) && x.LocationY == centerMap.LocationY);
            if (left != null)
            {
                list.Add(left);
            }
            var leftTop = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX - 1) && x.LocationY == (centerMap.LocationY + 1));
            if (leftTop != null)
            {
                list.Add(leftTop);
            }
            var leftBottom = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX - 1) && x.LocationY == (centerMap.LocationY - 1));
            if (leftBottom != null)
            {
                list.Add(leftBottom);
            }
            var right = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX + 1) && x.LocationY == centerMap.LocationY);
            if (right != null)
            {
                list.Add(right);
            }
            var rightTop = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX + 1) && x.LocationY == (centerMap.LocationY + 1));
            if (rightTop != null)
            {
                list.Add(rightTop);
            }

            var rightBottom = neerMaps.FirstOrDefault(x => x.LocationX == (centerMap.LocationX + 1) && x.LocationY == (centerMap.LocationY - 1));
            if (rightBottom != null)
            {
                list.Add(rightBottom);
            }
            return list;
        }


    }
}
