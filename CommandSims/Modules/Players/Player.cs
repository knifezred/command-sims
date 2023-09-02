using CommandSims.Enums;
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

        public GenderEnum Gender { get; set; }

        public RaceEnum Race { get; set; }

        public int Exp { get; set; }

        public int Speed { get; set; }

        public int Lucky { get; set; }


        public int HP { get; set; }

        public int MP { get; set; }

        public int HPMax
        {
            get
            {
                // 装备
                // 技能
                return this.HP + 10;
            }
        }

        public int MPMax
        {
            get
            {
                return this.MP + 10;
            }
        }


        public void Eat(int itemId)
        {

        }

        public void Sleep(int minites)
        {

        }

        public void Attack()
        {
            // 选择对象
            // 
        }

        public void Talk()
        {

        }

        public void DoingNothing()
        {

        }

        public void Training()
        {

        }

    }
}
