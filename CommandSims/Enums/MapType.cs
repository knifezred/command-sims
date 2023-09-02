using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    [Description("地图类型")]
    public enum MapType
    {
        [Display(Name = "道路")]
        Road = 0,
        [Display(Name = "建筑")]
        Building,
        [Display(Name = "屋子")]
        Room,
        [Display(Name = "山")]
        Mountain,
        [Display(Name = "河")]
        River,
        [Display(Name = "湖")]
        Lake,
        [Display(Name = "海")]
        Sea,
        [Display(Name = "平原")]
        Plain,
        [Display(Name = "洞穴")]
        Cave,
        [Display(Name = "峡谷")]
        Canyon


    }
}
