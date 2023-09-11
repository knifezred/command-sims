using CommandSims.Core;
using CommandSims.Entity.Base;
using CommandSims.Enums;
using CommandSims.Modules.Players;
using CommandSims.Modules.Talents;
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
        public Player()
        {
            this.Attribute = new PlayerAttribute();
        }

        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum Gender { get; set; }
        /// <summary>
        /// 种族
        /// </summary>
        public RaceEnum Race { get; set; }
        /// <summary>
        /// 七元属性
        /// </summary>
        public PlayerAttribute Attribute { get; set; }
        /// <summary>
        /// 天赋
        /// </summary>
        public List<Talent> Talents { get; set; }

        /// <summary>
        ///  经验值
        /// </summary>
        public int Exp { get; set; }

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

        public void ActiveTalent(Talent talent)
        {
            if (this.Talents == null)
            {
                this.Talents = new List<Talent>();
            }
            this.Talents.Add(talent);
            if (talent.Effects.Any())
            {
                Sims.Game.ActiveEffects(talent.Effects, this.Id, talent);
            }
        }

    }
}
