using CommandSims.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Maps
{
    public class MapEntity
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        /// 是否上锁
        /// true 不可进入，不可退出
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// 是否是出口
        /// </summary>
        public bool IsExit { get; set; }

        /// <summary>
        /// 出口连接地图id
        /// </summary>
        public int ExitMapId { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MapType Type { get; set; }

        /// <summary>
        /// 位置坐标
        /// </summary>
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }
}
