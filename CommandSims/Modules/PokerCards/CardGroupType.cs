using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public enum CardGroupType
    {
        [Display(Name = "单张")]
        Single = 1,
        [Display(Name = "对子")]
        Double,
        [Display(Name = "连对")]
        MultipleDouble,
        [Display(Name = "三不带")]
        Triple,
        [Display(Name = "三带一")]
        TripleWithOne,
        [Display(Name = "三带二")]
        TripleWithTwo,
        [Display(Name = "顺子")]
        Shunzi,
        [Display(Name = "飞机1")]
        MultipleTripleWithOne,
        [Display(Name = "飞机2")]
        MultipleTripleWithTwo,
        [Display(Name = "不符合规则")]
        BadCard,
        [Display(Name = "炸弹")]
        Boom = 999

    }
}
