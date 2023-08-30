using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    public enum PlayerActionEnum
    {
        [Display(Name = "什么也不做")]
        DoNothing = 0,
        [Display(Name = "攻击")]
        Attack,
        [Display(Name = "修炼")]
        Practice,
        [Display(Name = "吃东西")]
        Eat,
        [Display(Name = "睡觉")]
        Sleep,
        [Display(Name = "交易")]
        Deal,
    }
}
