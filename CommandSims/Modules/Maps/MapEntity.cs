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

        public bool CanOut { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public MapType Type { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public int LocationX { get; set; }
        public int LocationY { get; set; }
    }
}
