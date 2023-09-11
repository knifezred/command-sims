using CommandSims.Entity.Base;
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

        public List<ItemEntity> AllItems;

        public ItemPool()
        {

            WeaponItems = new List<WeaponItemEntity>();
            AllItems = new List<ItemEntity>();
        }

        public void InitAllItems()
        {
            AllItems.Clear();
            AllItems.Add(new ItemEntity()
            {
                Id = 1,
                Name = "金元",
                Description = "俗世流通的货币",
                Type = ItemType.Sundries,
                Level = GradeEnum.Orange1,
                Weight = 0.01,
                Price = 1,
            });
            AllItems.Add(new ItemEntity()
            {
                Id = 2,
                Name = "灵石",
                Description = "蕴含灵气的石头",
                Type = ItemType.Sundries,
                Level = GradeEnum.Yellow1,
                Weight = 1,
                Price = 10000,
            });
        }

        public void InitWeaponItems()
        {
            WeaponItems = new List<WeaponItemEntity>
            {
                new WeaponItemEntity()
                {
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
