using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Items
{
    public class ItemPool
    {
        public List<WeaponItemEntity> WeaponItems;

        public ItemPool() { }

        public void InitWeaponItems()
        {
            WeaponItems = new List<WeaponItemEntity>
            {
                new WeaponItemEntity()
                {
                    Id= 1,
                    Name="练习木剑",
                    Description="即使是天才也要从一把木剑开始",
                    Level=GradeEnum.DarkGreen,
                    Price=10,
                    Weight=1.0,
                    Category="",
                    Type=ItemType.Weapon
                }
            };

        }
    }
}
