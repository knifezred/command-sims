using CommandSims.Core;
using CommandSims.Entity;
using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Maps
{
    public class WorldMap
    {
        private static List<MapEntity> MapList => Sims.Context.WorldData.Maps;

        public WorldMap()
        {
            InitMapData();
        }

        public void InitMapData()
        {
            // 存档不存在Maps数据时加载默认数据
            if (!Sims.Context.WorldData.Maps.Any())
            {
                var tree = new TreeNode<MapEntity>(new MapEntity()
                {
                    Id = 0,
                    ParentId = 0,
                    Name = "ROOT"
                });
                tree.AddChild(new MapEntity()
                {
                    Id = 1,
                    LocationX = 0,
                    LocationY = 0,
                    Name = "小黑屋",
                    Description = "一座残破的小木屋",
                    Type = MapType.Building,
                    ExitMapId = 0,
                    IsExit = false,
                    Locked = true,
                });
                tree.Children[0].AddChildren(new List<MapEntity>() {
                new MapEntity()
                {
                    Id =2,
                    Name = "客厅-前",
                    Description = "一间漆黑的房间",
                    LocationX = 0,
                    LocationY = 0,
                    IsExit = true,
                    Locked = true,
                    Type = MapType.Room,
                },new MapEntity()
                {
                    Name = "客厅-中",
                    Description = "一间漆黑的房间",
                    LocationX = 0,
                    LocationY = 1,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                },new MapEntity()
                {
                    Name = "客厅-中",
                    Description = "一间漆黑的房间",
                    LocationX = 0,
                    LocationY = 2,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                },new MapEntity()
                {
                    Name = "客厅-后",
                    Description = "一间漆黑的房间",
                    LocationX = 0,
                    LocationY = 3,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                }});
                tree.Children[0].AddChild(new MapEntity()
                {
                    Name = "卧室1",
                    Description = "简陋的卧室，只有一张床",
                    LocationX = 1,
                    LocationY = 1,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                });
                tree.Children[0].AddChild(new MapEntity()
                {
                    Name = "卧室2",
                    Description = "简陋的卧室，只有一张床",
                    LocationX = 1,
                    LocationY = 2,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                });
                tree.Children[0].AddChild(new MapEntity()
                {
                    Name = "阳台",
                    Description = "堆满杂物",
                    LocationX = -1,
                    LocationY = 2,
                    IsExit = false,
                    Locked = false,
                    Type = MapType.Room,
                });

                tree.ResetAutoId();
                tree.TraverseWithAutoId(tree, AddMapNode);
            }
        }

        public void AddMapNode(MapEntity node)
        {
            if (node.Id > 0)
            {
                Sims.Context.WorldData.Maps.Add(node);
            }
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
                return MapList.First();
            }
            return map;
        }

        /// <summary>
        /// 是否为可进入房间
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public bool CanEnter(MapEntity map)
        {
            return MapList.Any(x => x.ParentId == map.Id && !x.Locked);
        }

        public bool CanExit(MapEntity map)
        {
            return !map.Locked && map.IsExit;
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public MapEntity GoMapEnterRoom(int mapId)
        {
            var rooms = MapList.Where(x => x.ParentId == mapId).ToList();
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
