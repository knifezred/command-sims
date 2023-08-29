using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity.Npc
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Player
    {
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }

    }
}
