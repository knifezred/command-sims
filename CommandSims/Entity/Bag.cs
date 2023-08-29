using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity
{
    /// <summary>
    /// 背包
    /// </summary>
    public class Bag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxCount { get; set; }
    }
}
