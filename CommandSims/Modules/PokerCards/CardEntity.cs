using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public class CardEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Point { get; set; }

        public CardSuit Suit { get; set; }

    }
}
