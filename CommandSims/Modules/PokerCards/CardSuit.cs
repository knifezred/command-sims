using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public enum CardSuit
    {
        [Display(Name = " ♠")]
        Spade = 100,
        [Display(Name = " ♥")]
        Heart = 200,
        [Display(Name = " ♦")]
        Diamond = 300,
        [Display(Name = " ♣")]
        Club = 400,
        [Display(Name = "🤡")]
        Poker = 999,
    }
}
