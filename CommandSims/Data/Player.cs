using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Data
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class Player
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 存档id
        /// </summary>
        [ForeignKey("player_archive_id_fk")]
        public int ArchiveId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }

    }
}
